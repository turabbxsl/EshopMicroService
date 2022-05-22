﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain.Models.ViewModels
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public string Description { get; set; }

        public DateTime CardExpiration { get; set; }
        public string CardExpirationShort { get; set; }
        public string CardHolderName { get; set; }
        public string CardSecurityNumber { get; set; }
        public string CardNumber { get; set; }
        public int CartTypeId { get; set; }



        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string ZipCode { get; set; }



        public string Buyer { get; set; }


        public void CardExpirationShortFormat()
        {
            CardExpirationShort = CardExpiration.ToString("MM/yy");
        }


        public void CardExpirationApiFormat()
        {
            var month = CardExpirationShort.Split('/')[0];
            var year = $"20{CardExpirationShort.Split('/')[1]}";

            CardExpiration = new DateTime(int.Parse(year), int.Parse(month), 1);
        }



    }

}
