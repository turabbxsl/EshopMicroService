using System.Collections.Generic;

namespace OrderService.Domain.Models
{
    public class Sebet
    {
        public string BuyId { get; set; } // <--- Bu sebetin sahibi hansi istifadecidir

        public List<SebetItem> SebetItems { get; set; }

        public Sebet()
        {

        }

        public Sebet(string customerId)
        {
            BuyId = customerId;
            SebetItems = new List<SebetItem>();
        }
    }



}
