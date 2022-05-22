using MediatR;
using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<bool>
    {

        public string Username { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime CardExpiration { get; set; }
        public string CardSecurityNumber { get; set; }
        public int CardTypeId { get; set; }



        private readonly List<OrderItemDTO> _orderItems;
        public IEnumerable<OrderItemDTO> OrderItems => _orderItems;



        public CreateOrderCommand()
        {
            _orderItems = new List<OrderItemDTO>();
        }





        public CreateOrderCommand(List<BasketService.API.Core.Domain.Models.SebetItem> sebetItems, string userid, string userName, string city, string street, string country, string zipCode, string cardNumber, string cardHolderNumber, DateTime expiration, string cardSecurityNumber, int cardTypeId) : this()
        {

            var dtoLists = sebetItems.Select(item => new OrderItemDTO()
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                PictureUrl = item.PictureUrl,
                UnitPrice = item.UnitPrice,
                Units = item.Quantity
            }).ToList();


            _orderItems = dtoLists;


            Username = userName;
            City = city;
            Street = street;
            Country = country;
            ZipCode = zipCode;
            CardNumber = cardNumber;
            CardHolderName = cardHolderNumber;
            CardExpiration = expiration;
            CardSecurityNumber = cardSecurityNumber;
            CardTypeId = cardTypeId;

        }

    }

    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
        public string PictureUrl { get; set; }
    }




}
