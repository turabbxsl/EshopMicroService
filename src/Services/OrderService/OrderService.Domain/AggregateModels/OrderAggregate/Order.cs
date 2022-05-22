using OrderService.Domain.AggregateModels.BuyerAgregate;
using OrderService.Domain.Events;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {

        //Bu sinif AggregateRoot modelidir ki,buna gore de IAggregateRoot-i implement etmisik

        public DateTime OrderDate { get; private set; }
        public int Quantity { get; private set; }
        public string Description { get; private set; }


        public Guid? BuyerId { get; private set; }
        public Buyer Buyer { get; private set; }


        public Address Address { get; private set; }


        private int orderStatusId;
        public OrderStatus OrderStatus { get; private set; }


        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;


        public Guid? PaymentMethodId { get; set; }


        protected Order()
        {
            Id = new Guid();
            _orderItems = new List<OrderItem>();
        }



        public Order(string username, Address address, int cardTypeId, string cardNumber, string       cardSecurityNumber,string cardHolderName, DateTime cardExpiration, Guid? paymentMethodId,
            Guid? buyerId = null) : this()
        {
            BuyerId = buyerId;
            orderStatusId = OrderStatus.Submitted.Id;
            OrderDate = DateTime.UtcNow;
            Address = address;
            PaymentMethodId = paymentMethodId;


            // Orderi yaratdiqda bir DomainEvent yaradilsin
            AddOrderStartedDomainEvent(username, cardTypeId, cardNumber, cardSecurityNumber,
                                       cardHolderName, cardExpiration);
        }



        private void AddOrderStartedDomainEvent(string username, int cardTypeId, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
        {

            var orderStartedDomainEvent = new OrderStartedDomainEvent(this, username, cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);

            this.AddDomainEvent(orderStartedDomainEvent);
        }






        public void AddOrderItem(int productId, string productName, decimal unitPrice, string pictureUrl, int units = 1)
        {
            // orderitem validations

            var orderItem = new OrderItem(productId, productName, pictureUrl, unitPrice, units);

            _orderItems.Add(orderItem);
        }



        public void SetBuyerId(Guid buyerId)
        {
            BuyerId = buyerId;
        }

        public void SetPaymentMethodId(Guid paymentMethodId)
        {
            PaymentMethodId = paymentMethodId;
        }
    }
}
