using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ApiGateway.Infrastructure;
using Web.ApiGateway.Services;
using Web.ApiGateway.Services.Interfaces;

namespace Web.ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddOcelot().AddConsul();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web.ApiGateway", Version = "v1" });
            });


            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<ISebetService, SebetService>();


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                   builder => builder.SetIsOriginAllowed((host) => true)
                                   .AllowAnyMethod()
                                   .AllowCredentials());
            });

            ConfigureHttpClient(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.ApiGateway v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseOcelot();
        }



        private void ConfigureHttpClient(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            // Biz nevaxt ki,BasketService getmek istesem ve ya basket adinda HttpClient yaradib BaseAddress-deki url ile birlikde biryere getmek istesem asagidaki HttpClientDelegatingHandler islesin
            services.AddHttpClient("basket", c =>
             {
                 c.BaseAddress = new Uri(Configuration["urls:basket"]);
             })
                .AddHttpMessageHandler<HttpClientDelegatingHandler>();





            // Biz nevaxt ki,CatalogService getmek istesem ve ya catalog adinda HttpClient yaradib BaseAddress-deki url ile birlikde biryere getmek istesem asagidaki HttpClientDelegatingHandler islesin
            services.AddHttpClient("catalog", c =>
            {
                c.BaseAddress = new Uri(Configuration["urls:catalog"]);
            })
                .AddHttpMessageHandler<HttpClientDelegatingHandler>();



        }


    }
}
