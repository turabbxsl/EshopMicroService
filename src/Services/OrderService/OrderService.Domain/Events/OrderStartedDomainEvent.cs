using MediatR;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Events
{
    public class OrderStartedDomainEvent:INotification
    {
        // BasketServisinden bir OrderCreated Eventi atilir ,OrderServisimiz o Eventi tutaraq Orderin surecini basladir

        //Create edilme prosesi BasketServisden gelir,Create olundu deye,Creat edilmisi OrderServisimiz tutaraq Orderin prosesi baslayir

        public string Username { get; set; }
        public int CardTypeId { get; set; }
        public string CardNumber { get; set; }
        public string CardSecurityNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime CardExpiration { get; set; }

        public Order Order { get; set; }

        public OrderStartedDomainEvent(Order order, string username, int cardTypeId, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
        {
            Order = order;
            Username = username;
            CardTypeId = cardTypeId;
            CardNumber = cardNumber;
            CardSecurityNumber = cardSecurityNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
        }

    }

}
