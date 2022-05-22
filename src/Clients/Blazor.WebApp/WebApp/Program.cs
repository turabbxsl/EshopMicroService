using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Application.Services;
using WebApp.Application.Services.Interfaces;
using WebApp.Infrastructure;
using WebApp.Utils;

namespace WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazoredLocalStorage();



            // Ne zaman ki AuthenticationStateProvider-e ehtiyac olsa,sen ona AuthStateProvider-i ver
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            // Ne zaman ki kiminse IIdentityService-e ehtiyac olsa,sen ona IdentityService-i ver
            builder.Services.AddTransient<IIdentityService, IdentityService>();
            builder.Services.AddTransient<ICatalogService, CatalogService>();
            builder.Services.AddTransient<ISebetService, SebetService>();
            builder.Services.AddTransient<IOrderService, OrderService>();



            // Biz sistemden HttpClient istesek, o gedecek "ApiGatewayHttpCLient" adi ile  client yaradacag
            builder.Services.AddScoped(sp =>
            {
                var clientFactory = sp.GetRequiredService<IHttpClientFactory>();

                return clientFactory.CreateClient("ApiGatewayHttpCLient");
            });



            builder.Services.AddSingleton<AppStateManager>();



            // Sistemde nevaxt ki "ApiGatewayHttpCLient" adi ile bir HttpClient istenilse,
            // bu clientin base url-i -->http://localhost:5000 
            builder.Services.AddHttpClient("ApiGatewayHttpCLient", client =>
             {
                 client.BaseAddress = new Uri("http://localhost:5000/");
             })
                .AddHttpMessageHandler<AuthTokenHandler>();
            // biz artiq HttpClient-i cagirdigimiz zaman onun BaseAdresinin ne oldugunu bilirik





            await builder.Build().RunAsync();
        }
    }
}
