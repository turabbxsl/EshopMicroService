using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Extensions
{
    public static class HostExtensions
    {

        public static IWebHost MigrateDBContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<TContext>>();

                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation($"Migration database associated with context {typeof(TContext).Name}");

                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(new TimeSpan[]
                        {
                            TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(8)
                        });

                    retry.Execute(() => InvokeSeeder(seeder, context, services));

                    logger.LogInformation($"Migration database associated with context {typeof(TContext).Name}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An Error occured while migration the database used on context");
                }
            }

            return host;
        }


        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.EnsureCreated(); //Database de tablelarin yaradilib yaradilmadigini tutur,eger yaradilmayibsa onlari yaradacaq

            context.Database.Migrate(); //Migration tapmis ise ve o migration calisdirilmayibsa o migrationu calisdiracag

            seeder(context, services);
        }




    }
}
