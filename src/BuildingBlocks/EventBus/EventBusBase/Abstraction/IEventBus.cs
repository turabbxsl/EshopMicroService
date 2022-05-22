using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base.Abstraction
{
    public interface IEventBus:IDisposable
    {

        void Publish(IntegrationEvent @event);
        void Subscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void UnSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;

    }
}
