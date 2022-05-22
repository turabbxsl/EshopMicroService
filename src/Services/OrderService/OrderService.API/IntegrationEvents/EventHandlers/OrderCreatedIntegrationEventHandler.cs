using EventBus.Base.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.API.IntegrationEvents.Events;
using OrderService.Application.Features.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.API.IntegrationEvents.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {

        //CreateOrderCommand-in Handlerini tetiklemek ucun CreateOrderCommand-i firlatmaq lazimdi

        private readonly IMediator mediator;
        private readonly ILogger<OrderCreatedIntegrationEventHandler> logger;




        public OrderCreatedIntegrationEventHandler(IMediator mediator, ILogger<OrderCreatedIntegrationEventHandler> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }





        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {

            logger.LogInformation($"Handling Integration Event : {@event.Id} at {typeof(Startup).Namespace} -- {@event}");

            var createOrderCommand = new CreateOrderCommand(@event.Sebet.SebetItems, @event.UserId, @event.Username, @event.City, @event.Street, @event.Country, @event.Zipcode, @event.CardNumber, @event.CartHolderName, @event.CartExpiration, @event.CartSecurityNumber, @event.CartTypeId);

            await mediator.Send(createOrderCommand);
        }
    }
}
