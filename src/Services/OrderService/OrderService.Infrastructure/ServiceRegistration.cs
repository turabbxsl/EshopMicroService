using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Infrastructure.Context;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure
{
    public static class ServiceRegistration
    {


        public static IServiceCollection AddPersistenceRegistration(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddDbContext<OrderDBContext>(options =>
            {
                options.UseSqlServer(configuration["OrderDBConnectionString"]);
                options.EnableSensitiveDataLogging();
            });

            service.AddScoped<IBuyerRepository, BuyerRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();

            var optionsBuilder = new DbContextOptionsBuilder<OrderDBContext>()
                                    .UseSqlServer(configuration["OrderDBConnectionString"]);

            using var dbContext = new OrderDBContext(optionsBuilder.Options, null);
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate(); //bekleyen migrationlari calistiracag

            return service;

        }


    }
}
