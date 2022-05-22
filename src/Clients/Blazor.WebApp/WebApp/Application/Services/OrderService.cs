using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Application.Services.DTOs;
using WebApp.Application.Services.Interfaces;
using WebApp.Domain.Models.ViewModels;

namespace WebApp.Application.Services
{
    public class OrderService : IOrderService
    {





        public SebetDTO MapOrderToSebet(Order order)
        {
            order.CardExpirationApiFormat();

            return new SebetDTO()
            {
                City = order.City,
                Street = order.Street,
                State = order.State,
                Country = order.Country,
                ZipCode = order.ZipCode,
                CardNumber = order.CardNumber,
                CardHolderName = order.CardHolderName,
                CardExpiration = order.CardExpiration,
                CardSecurityNumber = order.CardSecurityNumber,
                CartTypeId = 1,
                Buyer = order.Buyer
            };
        }
    }
}
