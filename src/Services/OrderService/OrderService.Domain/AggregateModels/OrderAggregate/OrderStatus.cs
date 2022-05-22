using OrderService.Domain.Exceptions;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class OrderStatus : Enumeration
    {

        public static OrderStatus Submitted = new(1, nameof(Submitted).ToLowerInvariant());    //Gonderildi
        public static OrderStatus AwaitingValidation = new(2, nameof(AwaitingValidation).ToLowerInvariant());   //Doğrulama Bekleniyor
        public static OrderStatus StockConfirmed = new(3, nameof(StockConfirmed).ToLowerInvariant());   //Stok onaylandi
        public static OrderStatus Paid = new(4, nameof(Paid).ToLowerInvariant());   //Odeme yapildi
        public static OrderStatus Shipped = new(5, nameof(Shipped).ToLowerInvariant());   //Karqolandi
        public static OrderStatus Cancelled = new(6, nameof(Cancelled).ToLowerInvariant());   //Iptal olundu


        public OrderStatus(int id, string name) : base(id, name)
        {

        }


        //Bir sifarisin hansi statuslari ola biler
        public static IEnumerable<OrderStatus> List()
        {
            return new[] { Submitted, AwaitingValidation, StockConfirmed, Paid, Shipped, Cancelled };
        }


        //Name-ni vererek Statusuna ulasa bildiyimiz metod
        //burada name dedikde Statuslarin Name-i nezerde tutulur.Basqa bir Name verilerse Xeta atiriq
        public static OrderStatus FromName(string name)
        {
            var state = List().SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            return state ?? throw new OrderingDomainException($"Possible values for OrderStatus : {String.Join(",", List().Select(s => s.Name))}");
        }


        //id-ni vererek Statusuna ulasa bildiyimiz metod
        //burada id dedikde Statuslarin ID-si nezerde tutulur.Basqa bir ID verilerse Xeta atiriq
        public static OrderStatus FromId(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            return state ?? throw new OrderingDomainException($"Possible values for OrderStatus : {String.Join(",", List().Select(s => s.Name))}");
        }


    }
}
