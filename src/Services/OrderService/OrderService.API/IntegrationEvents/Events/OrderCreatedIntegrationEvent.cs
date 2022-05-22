using BasketService.API.Core.Domain.Models;
using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.API.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {

        // BasketServisimiz EventBusa OrderCreated eventini  xeber verirdi.Bizde hemin bu eventi yeni,OrderCreated-i alib biz OrderService olaraq Sifarisin yerine yetirilmesine basliyacaq 

        public string UserId { get; set; }
        public string Username { get; set; }
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


        public Guid RequestId { get; set; }


        public Sebet Sebet { get; set; }




        public OrderCreatedIntegrationEvent(string userId, string username, string city, string street, string state, string country, string zipcode, string cardNumber, string cartHolderName, DateTime cartExpiration, string cartSecurityNumber, int cartTypeId, string buyer, Sebet sebet, Guid requestId)
        {
            UserId = userId;
            Username = username;

            City = city;
            Street = street;
            State = state;
            Country = country;
            Zipcode = zipcode;
            CardNumber = cardNumber;
            CartHolderName = cartHolderName;
            CartExpiration = cartExpiration;
            CartSecurityNumber = cartSecurityNumber;

            CartTypeId = cartTypeId;
            Buyer = buyer;

            RequestId = requestId;

            Sebet = sebet;
        }


    }
}
