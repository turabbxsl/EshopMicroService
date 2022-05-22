﻿using EventBus.Base.Abstraction;
using EventBus.Base.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PaymentService.API.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.API.IntegrationEvents.EventHandlers
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {

        private readonly IConfiguration _configuration;
        private readonly IEventBus _eventBus;
        private readonly ILogger<OrderStartedIntegrationEventHandler> _logger;


        public OrderStartedIntegrationEventHandler(IConfiguration configuration, IEventBus eventBus, ILogger<OrderStartedIntegrationEventHandler> logger)
        {
            _configuration = configuration;
            _eventBus = eventBus;
            _logger = logger;
        }


        public Task Handle(OrderStartedIntegrationEvent @event)
        {

            //Fake payment process

            string keyword = "PaymentSuccess";

            bool paymentSuccessFlag = _configuration.GetValue<bool>(keyword);

            //Odenisin bittiyini bildiren bir Event gonderirik
            IntegrationEvent paymentEvent = paymentSuccessFlag
                ? new OrderPaymentSuccessIntegrationEvent(@event.OrderId)
                : new OrderPaymentFailedIntegrationEvent(@event.OrderId, "This is a fake error message");

            _logger.LogInformation($"OrderStartedIntegrationEventHandler in PaymentService is fired with PaymentSuccess : {paymentSuccessFlag} , OrderID : {@event.OrderId}");


            //Servisimiz Event Gonderdiyi Zaman Publish istifade edecek
            _eventBus.Publish(paymentEvent);

            return Task.CompletedTask;
        }
    }
}
