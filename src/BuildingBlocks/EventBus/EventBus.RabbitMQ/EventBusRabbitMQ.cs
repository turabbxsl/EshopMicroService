using EventBus.Base;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {

        RabbitMQPersistentConnection persistentConnection;
        private readonly IConnectionFactory connectionFactory;
        private readonly IModel consumerChannel;

        public EventBusRabbitMQ(EventBusConfig config, IServiceProvider serviceProvider) : base(serviceProvider, config)
       {
            if (config.Connection != null)
            {
                var connJson = JsonConvert.SerializeObject(EventBusConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(connJson);
            }
            else
            {
                connectionFactory = new ConnectionFactory();
            }


            persistentConnection = new RabbitMQPersistentConnection(connectionFactory, config.ConnectionRetryCount);

            consumerChannel = CreateConsumerChannel();

            subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }




        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            eventName = ProcessEventName(eventName);

            if (!persistentConnection.IsConnection)
            {
                persistentConnection.TryConnect();
            }

            consumerChannel.QueueUnbind(queue: eventName,
                                        exchange: EventBusConfig.DefaultTopicName,
                                        routingKey: eventName
                    );

            if (subsManager.IsEmpty)
            {
                consumerChannel.Dispose();
            }

        }





        public override void Publish(IntegrationEvent @event)
        {

            if (!persistentConnection.IsConnection)
            {
                persistentConnection.TryConnect();
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(EventBusConfig.ConnectionRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                   {
                       //log
                   });

            var eventName = @event.GetType().Name;
            eventName = ProcessEventName(eventName);

            //exhange yaradilir
            consumerChannel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName,
                                            type: "direct");


            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properties = consumerChannel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                //QueueDeclare methodu ile yeni bir queue tanimliyoruz.
               /* consumerChannel.QueueDeclare(queue: GetSubName(eventName),
                                             durable: true,
                                             autoDelete: false,
                                             exclusive: false,
                                             arguments: null
                                             );


                consumerChannel.QueueBind(queue: GetSubName(eventName),
                                             exchange: EventBusConfig.DefaultTopicName,
                                             routingKey: eventName
                       );*/


                //Publish metodunda yalniz ilgili exchange-e gondermeliyik.Datamizi qarsi terefe gonderirik
                consumerChannel.BasicPublish(exchange: EventBusConfig.DefaultTopicName,
                                             routingKey: eventName,
                                             mandatory: true,
                                             basicProperties: properties,
                                             body: body
                                             );

            });
        }


        public override void Subscription<T, TH>()
        {

            var eventName = typeof(T).Name;
            eventName = ProcessEventName(eventName);

            if (!subsManager.HasSubsctiptionsForEvent(eventName))
            {

                //connection yoxlama
                if (!persistentConnection.IsConnection)
                {
                    persistentConnection.TryConnect();
                }

                //queue yaratdiq
                consumerChannel.QueueDeclare(queue: GetSubName(eventName),
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null
                    );

                consumerChannel.QueueBind(queue: GetSubName(eventName),
                                          exchange: EventBusConfig.DefaultTopicName,
                                          routingKey: eventName
                    );
            }

            subsManager.AddSubscription<T, TH>();
            StartBasicConsume(eventName);
        }


        public override void UnSubscription<T, TH>()
        {
            subsManager.RemovedSubscription<T, TH>();
        }



        private IModel CreateConsumerChannel()
        {
            if (!persistentConnection.IsConnection)
            {
                persistentConnection.TryConnect();
            }

            var channel = persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");

            return channel;
        }







        private void StartBasicConsume(string eventName)
        {
            if (consumerChannel != null)
            {
                var consumerr = new EventingBasicConsumer(consumerChannel);

                //Received event'i sürekli listen modunda olacaktır.
                consumerr.Received += Consumer_Received;

                //basic bir şekilde verilmiş olan Queue ismine göre mesajları alma işlemini başlatıyoruz.
                consumerChannel.BasicConsume(
                    queue: GetSubName(eventName),
                    autoAck: false,
                    consumer: consumerr
                    );
            }
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            eventName = ProcessEventName(eventName);

            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                // logging
            }

            consumerChannel.BasicAck(e.DeliveryTag, multiple: false);
        }
    }
}
