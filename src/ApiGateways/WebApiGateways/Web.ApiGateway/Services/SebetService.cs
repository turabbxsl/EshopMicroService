using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.ApiGateway.Extension;
using Web.ApiGateway.Models.Sebet;
using Web.ApiGateway.Services.Interfaces;

namespace Web.ApiGateway.Services
{
    public class SebetService : ISebetService
    {


        private readonly IHttpClientFactory httpClientFactory;

        public SebetService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }




        public async Task<SebetDTO> GetById(string id)
        {
            var client = httpClientFactory.CreateClient("basket");

            var response = await client.GetResponseAsync<SebetDTO>(id);

            return response ?? new SebetDTO(id);
        }




        public async Task<SebetDTO> UpdateAsync(SebetDTO currentSebet)
        {
            var client = httpClientFactory.CreateClient("basket");

            return await client.PostGetResponceAsync<SebetDTO, SebetDTO>("update", currentSebet);

        }


    }
}
