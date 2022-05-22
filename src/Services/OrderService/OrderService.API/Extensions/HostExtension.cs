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

namespace OrderService.API.Extensions
{
    public static class HostExtension
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
                    logger.LogInformation($"Migrating database associated with context -> {typeof(TContext).Name}");


                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(new TimeSpan[] {
                        TimeSpan.FromSeconds(3), //ilk 3 saniye denesin
                        TimeSpan.FromSeconds(5), //olmadisa 5 saniye dene
                        TimeSpan.FromSeconds(8), //oda olmadisa 8 saniye dene --- yeni 3 defe denesin
                        });


                    //Neyi deniyecek bes,oda asagidakidir
                    retry.Execute(() => InvokeSeeder(seeder, context, services));

                    logger.LogInformation("Migrated database");
                }
                catch (Exception ex)
                {
                    logger.LogError($"An Error occured while migration the database used on context -> {typeof(TContext).Name}");
                }

                return host;
            }
        }



        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            // Bu Database-in yaradilib yaradilmadigindan emin olacaq,yaradilmayibsa yaradacag
            context.Database.EnsureCreated();

            // Programi ayaga qaldirarken bir Migration varsa get o Migration prosesini ele
            context.Database.Migrate();

            seeder(context, services);
        }



    }
}
