using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ApiGateway.Models.Sebet;

namespace Web.ApiGateway.Services.Interfaces
{
    public interface ISebetService
    {

        Task<SebetDTO> GetById(string id);
        Task<SebetDTO> UpdateAsync(SebetDTO currentSebet);

    }
}
