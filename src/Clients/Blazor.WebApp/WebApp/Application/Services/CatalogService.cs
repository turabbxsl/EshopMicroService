using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Application.Services.Interfaces;
using WebApp.Domain.Models;
using WebApp.Domain.Models.CatalogModels;
using WebApp.Extensions;

namespace WebApp.Application.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient httpClient;

        public CatalogService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }



        public async Task<PaginatedItemsViewModel<CatalogItem>> GetCatalogItems()
        {
            // /catalog/items-den geriye bize PaginatedItemsViewModel<CatalogItem> donur,ona gore o datani elde etmek ucun bizim servisimizdeki PaginatedItemsViewModel<CatalogItem> sinfini yaziriq
            var response = await httpClient.GetResponseAsync<PaginatedItemsViewModel<CatalogItem>>("/catalog/items");

            return response;
        }
    }
}
