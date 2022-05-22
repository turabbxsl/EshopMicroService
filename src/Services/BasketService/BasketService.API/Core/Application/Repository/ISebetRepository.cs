using BasketService.API.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.API.Core.Application.Repository
{
    public interface ISebetRepository
    {
        Task<Sebet> GetSebetAsync(string customerId);

        IEnumerable<string> GetUsers();

        Task<Sebet> UpdateSebetAsync(Sebet sebet);

        Task<bool> DeleteSebetAsync(string id);
    }
}
