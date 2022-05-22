using Microsoft.Extensions.DependencyInjection;
using OrderService.API.IntegrationEvents.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.API.Extensions.Registration.EventHandlerRegistration
{
    public static class EventHandlerRegistration
    {

        // OrderCreatedIntegrationEvent-i Transient olaraq elave etdik ki,Bizim api-miz EventBus-a bir Event geldiyi zaman bu Eventi alib burdaki EventHandleri (OrderCreatedEventHandler) yarada bilsin


        public static IServiceCollection ConfigureEventHandlers(this IServiceCollection services)
        {
            services.AddTransient<OrderCreatedIntegrationEventHandler>();

            return services;
        }



    }
}
