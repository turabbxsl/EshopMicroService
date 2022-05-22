using OrderService.Domain.SeedWork;

namespace OrderService.Domain.AggregateModels.BuyerAgregate
{
    public class CartType : Enumeration
    {

        // Enumeration ----> id-den ve name-den bir-birileri arasinda cevirme,bulma,list icerisinde var mi yoxmu kontrolu ede bilmek ucun yaratdigimiz helperdir

        public static CartType Amex = new(1, nameof(Amex));
        public static CartType Visa = new(2, nameof(Visa));
        public static CartType MasterCard = new(3, nameof(MasterCard));

        public CartType(int id, string name) : base(id, name)
        {

        }

    }

}
