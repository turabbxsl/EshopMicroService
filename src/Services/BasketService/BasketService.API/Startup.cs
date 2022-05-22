using BasketService.API.Core.Application.Repository;
using BasketService.API.Core.Application.Services;
using BasketService.API.Extensions;
using BasketService.API.Infrastructure.Repository;
using BasketService.API.IntegrationEvents.EventHandlers;
using BasketService.API.IntegrationEvents.Events;
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
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.API
{
    public class Startup
    {
        private readonly ILogger<Startup> logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            this.logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesExt(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketService.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {

            logger.LogInformation("System Up and Running - from Configure");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketService.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.RegisterWithConsul(lifetime, Configuration);

            ConfigureSubscription(app.ApplicationServices);
        }

        private void ConfigureServicesExt(IServiceCollection services)
        {
            services.ConfigureAuth(Configuration);
            services.AddSingleton(sp => sp.ConfigureRedis(Configuration));

            services.ConfigureConsul(Configuration);

            services.AddHttpContextAccessor();
            /*services.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Debug);
            });*/



            services.AddTransient<ISebetRepository, RedisSebetRepository>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new EventBusConfig()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscripClientAppName = "BasketService",
                    EventBusType = EventBusType.RabbitMQ,
                    Connection = new ConnectionFactory()
                    {
                        HostName = "c_rabbitmq"
                    }
                };

                return EventBusFactory.Create(config, sp);
            });

            //EventBus-miz bundan bir kopya alaraq ise baslayacag
            services.AddTransient<OrderCreatedİntegrationEventHandler>();
        }


        private void ConfigureSubscription(IServiceProvider serviceProvider)
        {
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();

            eventBus.Subscription<OrderCreatedIntegrationEvent, OrderCreatedİntegrationEventHandler>();
        }
    }
}
