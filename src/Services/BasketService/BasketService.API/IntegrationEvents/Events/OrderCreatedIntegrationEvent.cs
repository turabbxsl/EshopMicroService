using BasketService.API.Core.Domain.Models;
using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.API.IntegrationEvents.Events
{

    //OrderCreatedIntegrationEvent-ni biz yaradacayig
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
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


        public Sebet Sebet { get; set; }



        public OrderCreatedIntegrationEvent(string userId, string username, string city, string street, string state, string country, string zipcode, string cardNumber, string cartHolderName, DateTime cartExpiration, string cartSecurityNumber, int cartTypeId, string buyer,Sebet sebet)
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
            Sebet = sebet;
        }


    }
}
