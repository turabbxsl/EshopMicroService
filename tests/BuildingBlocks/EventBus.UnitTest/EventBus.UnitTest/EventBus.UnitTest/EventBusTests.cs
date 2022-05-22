using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using EventBus.UnitTest.Events.EventHandlers;
using EventBus.UnitTest.Events.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace EventBus.UnitTest
{
    [TestClass]
    public class EventBusTests
    {

        private ServiceCollection services;

        public EventBusTests()
        {
            services = new ServiceCollection();
            services.AddLogging(configura => configura.AddConsole());
        }


        [TestMethod]
        public void Subscribe_event_on_rabbitmq_test()
        {

            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(GetRabbitMQConfig(), sp);
            });

            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            //OrderCreatedIntegrationEvent istek geldiyi zaman,bir mesaj gonderildiyi zaman bu OrderCreatedIntegrationEventHandler-i istifade et
            eventBus.Subscription<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();

            //eventBus.UnSubscription<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
        }







        [TestMethod]
        public void Subscribe_event_on_Azure_test()
        {

            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(GetAzureConfig(), sp);
            });

            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            //OrderCreatedIntegrationEvent istek geldiyi zaman,bir mesaj gonderildiyi zaman bu OrderCreatedIntegrationEventHandler-i istifade et
            eventBus.Subscription<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();

            //eventBus.UnSubscription<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();

            Task.Delay(2000).Wait();
        }


        [TestMethod]
        public void send_message_to_rabbitmq_test()
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(GetRabbitMQConfig(), sp);
            });

            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            //mesaj gonderirik
            eventBus.Publish(new OrderCreatedIntegrationEvent(5, "Turab"));
        }


        [TestMethod]
        public void send_message_to_azure_test()
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(GetAzureConfig(), sp);
            });

            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            //mesaj gonderirik
            eventBus.Publish(new OrderCreatedIntegrationEvent(5, "Xeyal"));
        }










        private EventBusConfig GetAzureConfig()
        {
            return new EventBusConfig()
            {
                ConnectionRetryCount = 5,
                SubscripClientAppName = "EventBus.UnitTest",
                DefaultTopicName = "TurabblTopicName",
                EventBusType = EventBusType.AzureServiceBus,
                EventNameSuffix = "IntegrationEvent",
                EventBusConnectionString = "Burada Azure Connection String i Olmalidir."
                /*Connection = new ConnectionFactory()
                {
                    HostName = "localhost",
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest"
                }*/
            };
        }

        private EventBusConfig GetRabbitMQConfig()
        {
            return new EventBusConfig()
            {
                ConnectionRetryCount = 5,
                SubscripClientAppName = "EventBus.UnitTest",
                DefaultTopicName = "TurabblTopicName",
                EventBusType = EventBusType.RabbitMQ,
                EventNameSuffix = "IntegrationEvent",
                /*Connection = new ConnectionFactory()
                {
                    HostName = "localhost",
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest"
                }*/
            };
        }


    }
}
