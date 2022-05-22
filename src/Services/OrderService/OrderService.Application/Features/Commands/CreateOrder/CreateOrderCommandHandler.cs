using EventBus.Base.Abstraction;
using MediatR;
using OrderService.Application.IntegrationEvents;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {

        private readonly IOrderRepository orderRepository;
        private readonly IEventBus eventBus;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IEventBus eventBus)
        {
            this.orderRepository = orderRepository;
            this.eventBus = eventBus;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var address = new Address(request.Street, request.City, request.State, request.Country, request.ZipCode);

            Order order = new Order(request.Username, address, request.CardTypeId, request.CardNumber, request.CardSecurityNumber, request.CardHolderName, request.CardExpiration, null);

            foreach (var orderItem in request.OrderItems)
            {
                order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice, orderItem.PictureUrl, orderItem.Units);
            }

            await orderRepository.AddAsync(order);
            await orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);


            //Order prosesi basladi
            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(request.Username);

            //OrderStartIntegrationEvent-e kim Subscript olubsa
            eventBus.Publish(orderStartedIntegrationEvent);

            return true;
        }
    }
}
