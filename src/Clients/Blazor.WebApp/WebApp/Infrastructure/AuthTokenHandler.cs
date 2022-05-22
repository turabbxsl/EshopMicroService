using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Extensions;

namespace WebApp.Infrastructure
{
    public class AuthTokenHandler : DelegatingHandler
    {


        private readonly ISyncLocalStorageService storageService;

        public AuthTokenHandler(ISyncLocalStorageService syncLocalStorage)
        {
            this.storageService = syncLocalStorage;
        }



        // Her hansi biryere istek gonderdiyimiz zaman,istek gonderilmeden evvelki isleyen yer buradi.Ona gore de istek gonderilerken tokeni de Set edirik
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (storageService != null)
            {

                // Eger Storage-de Tokenimiz var ise bunu Headerin Authorization-na bunu elave edirik
                var token = storageService.GetToken();
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }






    }
}
