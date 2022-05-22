using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.UnitTest.Events.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public OrderCreatedIntegrationEvent(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
