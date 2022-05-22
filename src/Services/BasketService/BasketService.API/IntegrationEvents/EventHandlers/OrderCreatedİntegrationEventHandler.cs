using BasketService.API.Core.Application.Repository;
using BasketService.API.IntegrationEvents.Events;
using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.API.IntegrationEvents.EventHandlers
{
    public class OrderCreatedİntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {

        private readonly ISebetRepository _repository;
        private readonly ILogger<OrderCreatedIntegrationEvent> _logger;

        public OrderCreatedİntegrationEventHandler(ISebetRepository repository, ILogger<OrderCreatedIntegrationEvent> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            _logger.LogInformation($"-----Handling integration event : {@event.Id} at BasketService.API");

            await _repository.DeleteSebetAsync(@event.UserId.ToString());
        }
    }
}
