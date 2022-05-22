using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Application.Services.DTOs;
using WebApp.Application.Services.Interfaces;
using WebApp.Domain.Models.ViewModels;
using WebApp.Extensions;

namespace WebApp.Application.Services
{
    public class SebetService : ISebetService
    {

        private readonly HttpClient httpClient;
        private readonly IIdentityService identityService;
        private readonly ILogger<SebetService> logger;

        public SebetService(HttpClient httpClient, IIdentityService identityService, ILogger<SebetService> logger)
        {
            this.httpClient = httpClient;
            this.identityService = identityService;
            this.logger = logger;
        }



        public async Task AddItemToSebet(int productId)
        {

            var model = new
            {
                CatalogItemId = productId,
                Quantity = 1,
                SebetId = identityService.GetUsername()
            };

            await httpClient.PostAsync("basket/items", model);

        }



        public Task Checkout(SebetDTO sebetDTO)
        {
            return httpClient.PostAsync("basket/checkout", sebetDTO);
        }



        public async Task<Sebet> GetSebet()
        {
            var response = await httpClient.GetResponseAsync<Sebet>("basket/" + identityService.GetUsername());

            return response ?? new Sebet() { BuyerId = identityService.GetUsername() };
        }



        public async Task<Sebet> UpdateSebet(Sebet sebet)
        {
            var response = await httpClient.PostGetResponceAsync<Sebet, Sebet>("basket/update", sebet);

            return response;
        }

    }
}
