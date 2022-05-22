using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Application.Services.DTOs;
using WebApp.Domain.Models.ViewModels;

namespace WebApp.Application.Services.Interfaces
{
    public interface IOrderService
    {
        SebetDTO MapOrderToSebet(Order order);

    }
}
