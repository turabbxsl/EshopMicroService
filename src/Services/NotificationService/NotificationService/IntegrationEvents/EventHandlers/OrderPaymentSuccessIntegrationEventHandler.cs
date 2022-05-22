using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using NotificationService.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.IntegrationEvents.EventHandlers
{
    public class OrderPaymentSuccessIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentSuccessIntegrationEvent>
    {

        private readonly ILogger<OrderPaymentSuccessIntegrationEvent> _logger;

        public OrderPaymentSuccessIntegrationEventHandler(ILogger<OrderPaymentSuccessIntegrationEvent> logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderPaymentSuccessIntegrationEvent @event)
        {
           
            //Send Fail Notification - (SMS,Email)

            _logger.LogInformation($"Order Payment Success with OrderId : {@event.OrderId}");

            return Task.CompletedTask;
        }
    }
}
