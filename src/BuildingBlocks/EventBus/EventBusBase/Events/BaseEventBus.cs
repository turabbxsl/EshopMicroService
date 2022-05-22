﻿using EventBus.Base.Abstraction;
using EventBus.Base.SubManager;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base.Events
{
    public abstract class BaseEventBus : IEventBus
    {

        public readonly IServiceProvider serviceProvider;
        public readonly IEventBusSubscriptionManager subsManager;

        public EventBusConfig EventBusConfig { get; set; }

        protected BaseEventBus(IServiceProvider serviceProvider, EventBusConfig eventBusConfig)
        {
            this.serviceProvider = serviceProvider;
            this.EventBusConfig = eventBusConfig;
            subsManager = new InMemoryEventBusSubscriptionManager(ProcessEventName);
        }


        public abstract void Publish(IntegrationEvent @event);

        public abstract void Subscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        public abstract void UnSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;





        public virtual string ProcessEventName(string eventName)
        {
            if (EventBusConfig.DeleteEventPrefix)
            {
                eventName = eventName.TrimStart(EventBusConfig.EventNamePrefix.ToArray());
            }

            if (EventBusConfig.DeleteEventSuffix)
            {
                eventName = eventName.TrimEnd(EventBusConfig.EventNameSuffix.ToArray());
            }

            return eventName;
        }

        public virtual string GetSubName(string eventName)
        {
            return $"{EventBusConfig.SubscripClientAppName}.{ProcessEventName(eventName)}";
        }


        public virtual void Dispose()
        {
            EventBusConfig = null;
            subsManager.Clear();
        }




        public async Task<bool> ProcessEvent(string eventName, string message)
        {
            eventName = ProcessEventName(eventName);

            var processed = false;

            if (subsManager.HasSubsctiptionsForEvent(eventName))
            {
                var subscriptions = subsManager.GetHandlersForEvent(eventName);

                using (var scope = serviceProvider.CreateScope())
                {
                    foreach (var sub in subscriptions)
                    {

                        var handler = serviceProvider.GetService(sub.HandleType);

                        if (handler == null)
                        {
                            continue;
                        }


                        var eventType = subsManager.GetEventTypeByName($"{EventBusConfig.EventNamePrefix}{eventName}{EventBusConfig.EventNameSuffix}");
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);


                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });

                    }
                }

                processed = true;
            }

            return processed;
        }

    }
}
