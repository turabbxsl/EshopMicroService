using MediatR;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.AggregateModels.BuyerAgregate;
using OrderService.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.DomainEventHandlers
{
    public class OrderStartedDomainEventHandler : INotificationHandler<OrderStartedDomainEvent>
    {

        //Domain layerdeki OrderStartedDomainEvent firlatildiginda bizim bu OrderStartedDomainEventHandler-imiz isleyecek

        private readonly IBuyerRepository buyerRepository;

        public OrderStartedDomainEventHandler(IBuyerRepository buyerRepository)
        {
            this.buyerRepository = buyerRepository;
        }



        public async Task Handle(OrderStartedDomainEvent orderStartedEvent, CancellationToken cancellationToken)
        {
            var cardTypeId = (orderStartedEvent.CardTypeId != 0) ? orderStartedEvent.CardTypeId : 1;

            //OrderStartedi basladan kimdise o evvelceden var mi bizde,varsa detaylarini getiririk ve getirerkende PaymentMethodlarini include ederek olari da getiririk
            var buyer = await buyerRepository.GetSingleAsync(i => i.Name == orderStartedEvent.Username, i => i.PaymentMethods);

            bool buyerOrginallyExisted = buyer != null;

            if (!buyerOrginallyExisted)
            {
                //yoxdursa yeni buyer yaradiriq
                buyer = new Buyer(orderStartedEvent.Username);
            }


            //istifadecinin odeme yontemi bizim sistemimizde var mi ve kredit kartinin dogrulanmasi proseslerini burada edirik
            buyer.VerifyOrAddPaymentMethod(orderStartedEvent.CardTypeId,
                                           $"Payment Method on {DateTime.UtcNow}",
                                           orderStartedEvent.CardNumber,
                                           orderStartedEvent.CardSecurityNumber,
                                           orderStartedEvent.CardHolderName,
                                           orderStartedEvent.CardExpiration,
                                           orderStartedEvent.Order.Id
                                           );

            var buyerUpdated = buyerOrginallyExisted ? buyerRepository.Update(buyer)
                                                     : await buyerRepository.AddAsync(buyer);

            await buyerRepository.UnitOfWork.SaveEntitiesAsync();

        }
    }
}
