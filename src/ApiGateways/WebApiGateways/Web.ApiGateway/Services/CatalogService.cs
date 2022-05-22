using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.ApiGateway.Extension;
using Web.ApiGateway.Models.Catalog;
using Web.ApiGateway.Services.Interfaces;

namespace Web.ApiGateway.Services
{
    public class CatalogService : ICatalogService
    {

        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public CatalogService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            // Artik httclient-in icerisinde Basketdirse Basket,Catalogdursa Batalog var

            this.configuration = configuration;
        }



        public Task<CatalogItem> GetCatalogItemAsync(int id)
        {
            // Clienti yaranan kimi Startupdaki AddHttpClient ise dusub baseAddresini verilir.
            var client = httpClientFactory.CreateClient("catalog");

            // Hemin clienta yeni CatalogService Get Isteyi atib geriye CatalogItem donecek
            var response = client.GetResponseAsync<CatalogItem>("/items/" + id.ToString());

            return response;
        }




        public Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }



    }
}
