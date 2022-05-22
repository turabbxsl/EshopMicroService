using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotificationService.IntegrationEvents.EventHandlers;
using NotificationService.IntegrationEvents.Events;
using RabbitMQ.Client;
using System;

namespace NotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            //Notification Service - Payment-in Successmi Failmi bilgisini alacag ve Eventbusa basqa birsey xeber vermesine gerek yoxdur


            ServiceCollection services = new ServiceCollection();

            ConfigureServices(services);

            var sp = services.BuildServiceProvider();

            IEventBus eventBus = sp.GetRequiredService<IEventBus>();

            //Biz EventBusdaki Bu Queue-lari dinlemek isteyirik

            //OrderPayment Success ve ya Failed oldugunda mene xeber ver, Xeber vererkende Bu EventHandler-leri Istifade et ve onlarin icerisindeki Handle metodlari islesin.

            eventBus.Subscription<OrderPaymentSuccessIntegrationEvent, OrderPaymentSuccessIntegrationEventHandler>();
            eventBus.Subscription<OrderPaymentFailedIntegrationEvent, OrderPaymentFailedIntegrationEventHandler>();


            Console.WriteLine($"Application is Running.");


            Console.ReadKey();
        }


        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(configure =>
            {
                configure.AddConsole();
            });

            services.AddTransient<OrderPaymentSuccessIntegrationEventHandler>();
            services.AddTransient<OrderPaymentFailedIntegrationEventHandler>();

            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new EventBusConfig()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscripClientAppName = "NotificationService",
                    EventBusType = EventBusType.RabbitMQ,
                    Connection = new ConnectionFactory()
                    {
                        HostName = "c_rabbitmq"
                    }
                };

                return EventBusFactory.Create(config, sp);
            });

        }






    }
}
