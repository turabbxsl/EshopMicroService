using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderService.API.Extensions.Registration.EventHandlerRegistration;
using OrderService.API.Extensions.Registration.ServiceDiscovery;
using OrderService.API.IntegrationEvents.EventHandlers;
using OrderService.API.IntegrationEvents.Events;
using OrderService.Application;
using OrderService.Infrastructure;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderService.API", Version = "v1" });
            });

            ConfigureService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderService.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.RegisterWithConsul(lifetime, Configuration);
            ConfigureEventBusForSubscription(app);
        }




        private void ConfigureService(IServiceCollection services)
        {

            services.AddLogging(configure => configure.AddConsole())
                    .AddApplicationRegistration(typeof(Startup)) // Application Layerindeki
                    .AddPersistenceRegistration(Configuration)   // Infrastrucure Layerindeki
                    .ConfigureEventHandlers()                    // Ele API-deki Extensiondir Layerdeki
                    .AddServiceDiscoveryRegistration(Configuration);


            services.AddSingleton(sp =>
            {
                EventBusConfig config = new EventBusConfig()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscripClientAppName = "OrderService",
                    EventBusType = EventBusType.RabbitMQ,
                    Connection = new ConnectionFactory()
                    { 
                    HostName = "c_rabbitmq"
                    }
                };

                return EventBusFactory.Create(config, sp);
            });
        }



        //Birdeki biz bir eventi dinleyeceyik,ona subscip olacayig
        private void ConfigureEventBusForSubscription(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            // OrderCreatedIntegrationEvent-e subscript ol,eger ise duserse ise OrderCreatedIntegrationEventHandler-in Handle metodu islesin
            eventBus.Subscription<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
        }



    }
}
