using OrderService.Domain.Exceptions;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.BuyerAgregate
{
    public class PaymentMethod : BaseEntity
    {

        //sen bunu hansi kredit karti ile odeyeceksen

        public string Alias { get; set; }
        public string CardNumber { get; set; }
        public string SecurityNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime Expiration { get; set; }


        public int CardTypeId { get; set; } //Visa mi yoxsa Master mi
        public CartType CardType { get; private set; }


        public PaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderNumber, DateTime expiration)
        {
            CardNumber = !string.IsNullOrWhiteSpace(cardNumber) ? cardNumber : throw new OrderingDomainException(nameof(cardNumber));
            SecurityNumber = !string.IsNullOrWhiteSpace(securityNumber) ? securityNumber : throw new OrderingDomainException(nameof(securityNumber));
            CardHolderName = !string.IsNullOrWhiteSpace(cardHolderNumber) ? cardHolderNumber : throw new OrderingDomainException(nameof(cardHolderNumber));


            if (expiration < DateTime.UtcNow)
            {
                throw new OrderingDomainException(nameof(expiration));
            }


            Alias = alias;
            Expiration = expiration;
            CardTypeId = cardTypeId;
        }



        public bool IsEqualTo(int cardTypeId, string cardNumber, DateTime expiration)
        {
            //Colden gelen ile bizimki bir birini tutur mu tutmur mu o kontrol edilecek burda

            return CardTypeId == cardTypeId
                && CardNumber == cardNumber
                && Expiration == expiration;
        }


    }








}
