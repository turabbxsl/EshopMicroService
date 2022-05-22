using CatalogService.API.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.API.Extensions;
using Microsoft.AspNetCore;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostbuilder = CreateHostBuilder(args);

            hostbuilder.MigrateDBContext<CatalogContext>((context, services) =>
            {
                var env = services.GetService<IWebHostEnvironment>();

                var logger = services.GetService<ILogger<CatalogContextSeed>>();

                new CatalogContextSeed()
                .SeedAsync(context, env, logger)
                .Wait();
            });

            hostbuilder.Run();
        }

        static IWebHost CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateOnBuild = false;
                    options.ValidateScopes = false;
                })
                .UseStartup<Startup>()
                .UseWebRoot("Pics")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build();
        }

    }
}
