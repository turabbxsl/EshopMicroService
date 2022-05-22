using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.API
{
    public class Program
    {

        // Sistemimizin ortami hansidi onu bize verir,yəni Development mi yoxsa Production mu oldugunu bilmek ucundur
        private static string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


        // Bu sistem ucun
        private static IConfiguration configuration
        {
            get
            {
                return new ConfigurationBuilder()
                        .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // yeni Path-i BasketService olsun

                        .AddJsonFile($"Configurations/appsettings.json", optional: false)  // mutleq Configurations folderinin altinda appsettings.json fayli olacaq,yeni sen hec bir fayl tapa bilmesen default olaraq get appsettings.json faylini yukle

                        .AddJsonFile($"Configurations/appsettings.{env}.json", optional: true)  // optional:true deyirikse, .NET Core ilk bu fayli yuklemeye calisacag eger tapsa yukleyecek,tapa bilmese ise yuxarida qeyd etdiyim appsettings.json faylini yukleyecek

                        .AddEnvironmentVariables()

                        .Build();
            }
        }



        // Bu ise Serilog ucun
        private static IConfiguration serilog_Configuration
        {
            get
            {
                return new ConfigurationBuilder()
                        .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // yeni Path-i BasketService olsun

                        .AddJsonFile($"Configurations/serilog.json", optional: false)  // mutleq Configurations folderinin altinda serilog.json fayli olacaq,yeni sen hec bir fayl tapa bilmesen default olaraq get serilog.json faylini yukle

                        .AddJsonFile($"Configurations/serilog.{env}.json", optional: true)  // optional:true deyirikse, .NET Core ilk bu fayli yuklemeye calisacag eger tapsa yukleyecek,tapa bilmese ise yuxarida qeyd etdiyim serilog.json faylini yukleyecek

                        .AddEnvironmentVariables()

                        .Build();
            }
        }




        // Bu bizim ozumuzun yaratdigi HostBuilder
        [Obsolete]
        public static IWebHost BuildWebHost(IConfiguration configuration, string[] args)
        {
            return WebHost.CreateDefaultBuilder()
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateOnBuild = false;
                    options.ValidateScopes = false;
                })
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration)) // Configurasiya cagrilanda bu configuration islesin dedik
                .UseStartup<Startup>()
                .ConfigureLogging(x => x.ClearProviders()) // sistemde evvelceden olan bir loglama varsa biz onu silib ustune SeriLog-u elave edirik
                .UseSerilog()


                .Build();
        }


        public static void Main(string[] args)
        {
            var host = BuildWebHost(configuration, args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(serilog_Configuration) // Fayli Configurasiyadan oxu
                .CreateLogger();

            host.Run();
        }


    }
}
