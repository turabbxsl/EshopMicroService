using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderService.API.Extensions;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace OrderService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            var host = BuildWebHost(GetConfiguration(), args);

            host.MigrateDBContext<OrderDBContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<OrderDBContext>>();

                var dbContextSeeeder = new OrderDBContextSeed();
                dbContextSeeeder.SeedAsync(context, logger).Wait();
            });
        }


        static IWebHost BuildWebHost(IConfiguration configuration, string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateOnBuild = false;
                    options.ValidateScopes = false;
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateOnBuild = false;
                })
                .ConfigureAppConfiguration(i => i.AddConfiguration(configuration))
                .UseStartup<Startup>()
                .Build();
        }



        static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();


            return builder.Build();
        }




        /*   public static IHostBuilder CreateHostBuilder(string[] args) =>
               Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseStartup<Startup>();
                   });*/
    }
}
