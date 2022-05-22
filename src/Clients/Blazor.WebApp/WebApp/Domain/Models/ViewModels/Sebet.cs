using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Domain.Models.ViewModels
{
    public class Sebet
    {


        public List<SebetItem> SebetItems { get; set; } = new List<SebetItem>();

        public string BuyerId { get; init; }

        public decimal Total()
        {
            return Math.Round(SebetItems.Sum(x => x.UnitPrice * x.Quantity));
        }

        public Sebet()
        {

        }

    }




}
