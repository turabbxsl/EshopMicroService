using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Application.Services.DTOs;
using WebApp.Domain.Models.ViewModels;

namespace WebApp.Application.Services.Interfaces
{
    public interface ISebetService
    {

        Task<Sebet> GetSebet();

        Task<Sebet> UpdateSebet(Sebet sebet);

        Task AddItemToSebet(int productId);

        Task Checkout(SebetDTO sebetDTO);
    }
}
