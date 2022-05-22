using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.Application.Services.Interfaces;
using WebApp.Domain.Models.Users;
using WebApp.Extensions;
using WebApp.Utils;

namespace WebApp.Application.Services
{
    public class IdentityService : IIdentityService
    {

        private readonly HttpClient httpClient;
        private readonly ISyncLocalStorageService syncLocalStorageService;
        private readonly AuthenticationStateProvider authStateProvider;

        public IdentityService(HttpClient httpClient, ISyncLocalStorageService syncLocalStorageService, AuthenticationStateProvider authStateProvider)
        {
            this.httpClient = httpClient;
            this.syncLocalStorageService = syncLocalStorageService;
            this.authStateProvider = authStateProvider;
        }



        public bool isLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUsername()
        {
            return syncLocalStorageService.GetUsername();
        }

        public string GetUserToken()
        {
            return syncLocalStorageService.GetToken();
        }

        public async Task<bool> Login(string username, string password)
        {
            var request = new UserLoginRequest(username, password);

            // Cole UserLoginRequest gonderirik ve senden UserLoginResponse gozleyirem
            var response = await httpClient.PostGetResponceAsync<UserLoginResponse, UserLoginRequest>("auth", request);


            // Eger Token doludursa sisteme basarili giris edib demekdir
            if (!string.IsNullOrEmpty(response.Token))
            {
                syncLocalStorageService.SetToken(response.Token);
                syncLocalStorageService.SetUsername(response.Username);


                // Biz hemcinin de sisteme de Giris edildi demeliyik
                ((AuthStateProvider)authStateProvider).NotifyUserLogin(response.Username);


                // Sisteme giris eden istifadeci HttpClient-i cagirarsa onun icerisindeki Authorization qisminde Tokeni yer alsin
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", response.Token);

                return true;
            }

            return false;
        }






        public void Logout()
        {
            syncLocalStorageService.RemoveItem("token");
            syncLocalStorageService.RemoveItem("username");


            // Sistemden istifadeci cixis etdi
            ((AuthStateProvider)authStateProvider).NotifyUserLogout();

            httpClient.DefaultRequestHeaders.Authorization = null;
        }



    }
}
