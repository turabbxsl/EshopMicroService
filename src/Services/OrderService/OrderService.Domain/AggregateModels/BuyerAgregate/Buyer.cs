using OrderService.Domain.Events;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.BuyerAgregate
{
    public class Buyer : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; } //Username


        private List<PaymentMethod> _paymentMethods;
        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods.AsReadOnly();


        protected Buyer()
        {
            _paymentMethods = new List<PaymentMethod>();
        }


        public Buyer(string name) : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }



        //Payment Metodun Verify edilmesi ucun bir metod
        public PaymentMethod VerifyOrAddPaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration, Guid orderId)
        {
            var existingPaymentMethod = _paymentMethods.SingleOrDefault(p => p.IsEqualTo(cardTypeId, cardNumber, expiration));


            //Sistemim icerisinde PaymentMethodlarimin icerisinde var mi yoxmu,eger var ise bu
            //Bu karti ve melumatlarini biz eger saxlamisiqsa bu
            if (existingPaymentMethod != null)
            {
                //Verify edilmis kartlar arasinda bu kart var,bu kodda biz onu deyirik
                AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, existingPaymentMethod, orderId));

                return existingPaymentMethod;
            }


            //Bu karti ve melumatlarini biz eger saxlaMAisiqsa bu
            var newPaymentMethod = new PaymentMethod(cardTypeId, alias, cardNumber, securityNumber, cardHolderName, expiration);
            _paymentMethods.Add(newPaymentMethod);


            //Bu kart verify edildi olaraq DomainEventimize elave edirik
            AddDomainEvent(new BuyerAndPaymentMethodVerifiedDomainEvent(this, newPaymentMethod, orderId));

            return newPaymentMethod;
        }



        public override bool Equals(object obj)
        {
            return base.Equals(obj) ||
                              (obj is Buyer buyer && Id.Equals(buyer.Id) && Name == buyer.Name);
        }

    }
}
