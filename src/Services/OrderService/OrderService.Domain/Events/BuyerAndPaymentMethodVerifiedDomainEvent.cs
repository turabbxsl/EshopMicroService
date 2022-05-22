using MediatR;
using OrderService.Domain.AggregateModels.BuyerAgregate;
using System;

namespace OrderService.Domain.Events
{
    public class BuyerAndPaymentMethodVerifiedDomainEvent : INotification
    {

        // Bu sifarisi veren kisinin ve odeme yonteminin dogrulanmasi ile elaqeli prosesleri edeceyimiz bir event

        // Kredi karti ile odemek isteyir ama Visa kartla.Bizim sistemimizde Visa kart ile odeme movcuddur mu

        // Sifarisi baslatmadan evvel satin alan adamin ve odeme yonteminin dogrulanmasi ucun bir eventdir.


        public Buyer Buyer { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid OrderId { get; set; }


        public BuyerAndPaymentMethodVerifiedDomainEvent(Buyer buyer, PaymentMethod paymentMethod, Guid orderId)
        {
            Buyer = buyer;
            PaymentMethod = paymentMethod;
            OrderId = orderId;
        }

        //Araya baska prosesleri de elave ede bilerik.Karqoya verilmesi,Karqo statuslari kimi ve.s

    }

}
