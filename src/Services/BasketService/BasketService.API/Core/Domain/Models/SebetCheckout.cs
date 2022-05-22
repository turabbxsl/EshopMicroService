using System;

namespace BasketService.API.Core.Domain.Models
{
    public class SebetCheckout
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string CardNumber { get; set; }
        public string CartHolderName { get; set; }
        public DateTime CartExpiration { get; set; }
        public string CartSecurityNumber { get; set; }
        public int CartTypeId { get; set; }

        public string Buyer { get; set; }

    }



}
