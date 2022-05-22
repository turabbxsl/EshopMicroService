using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Extensions;

namespace WebApp.Utils
{
    public class AuthStateProvider : AuthenticationStateProvider
    {

        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationState anonymous;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService LocalStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = LocalStorageService;
            anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }


        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            String apiToken = await localStorageService.GetToken();

            if (String.IsNullOrEmpty(apiToken))
            {
                return anonymous;
            }

            String username = await localStorageService.GetUsername();

            var cp = new ClaimsPrincipal(new ClaimsIdentity(new[] {

                new Claim(ClaimTypes.Name,username)

            }, "JwtAuthType"));


            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

            return new AuthenticationState(cp);
        }



        // Sisteme istifadecinin state-ni bildirmeliyik ki,istifadecinin giris edip etmediyi bilgisini tuta bilek
        public void NotifyUserLogin(String username)
        {
            var cp = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name,username)
                    }, "jwtAuthType"));

            var authState = Task.FromResult(new AuthenticationState(cp));

            // Sadece bizim tutmagimiz kifayet deyil, ona gore burda sisteme de yeni Blazor-un ozune de deyirik ki,bu istifadeci GIRIS etti
            NotifyAuthenticationStateChanged(authState);
        }



        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(anonymous);

            // Sadece bizim tutmagimiz kifayet deyil, ona gore burda sisteme de yeni Blazor-un ozune de deyirik ki,bu istifadeci CIXIS etti
            NotifyAuthenticationStateChanged(authState);
        }


    }
}
