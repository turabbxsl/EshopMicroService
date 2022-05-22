using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderService.Domain.AggregateModels.BuyerAgregate;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Domain.SeedWork;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Context
{
    public class OrderDBContextSeed
    {

        public async Task SeedAsync(OrderDBContext dBContext, ILogger<OrderDBContext> logger)
        {

            var policy = CreatePolicy(logger, nameof(OrderDBContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var useCustomizationData = false;
                var contentRootPath = "Seeding/Setup";

                using (dBContext)
                {

                    //evvelce database-e gedib bekleyen bir migration varsa migrationumuzu edeceyik
                    dBContext.Database.Migrate();


                    if (!dBContext.CartTypes.Any())
                    {
                        dBContext.CartTypes.AddRange(useCustomizationData
                                                     ? GetCartTypesFromFile(contentRootPath, logger)
                                                     : GetPredefinedCartTypes());
                    }

                    if (!dBContext.OrderStatuses.Any())
                    {
                        dBContext.OrderStatuses.AddRange(useCustomizationData
                                                     ? GetOrderStatusFromFile(contentRootPath, logger)
                                                     : GetPredefinedOrderStatus());
                    }

                    await dBContext.SaveChangesAsync();
                }
            });

        }



        private IEnumerable<CartType> GetCartTypesFromFile(string contentRootPath, ILogger<OrderDBContext> logger)
        {
            string fileName = "CartTypes.txt";

            if (!File.Exists(fileName))
            {
                return GetPredefinedCartTypes();
            }

            var fileContent = File.ReadAllLines(fileName);

            int id = 1;
            var list = fileContent.Select(x => new CartType(id++, x)).Where(x => x != null);

            return list;
        }
        private IEnumerable<CartType> GetPredefinedCartTypes()
        {
            return Enumeration.GetAll<CartType>();
        }





        private IEnumerable<OrderStatus> GetOrderStatusFromFile(string contentRootPath, ILogger<OrderDBContext> logger)
        {
            string fileName = "OrderStatus.txt";

            if (!File.Exists(fileName))
            {
                return GetPredefinedOrderStatus();
            }

            var fileContent = File.ReadAllLines(fileName);

            int id = 1;
            var list = fileContent.Select(x => new OrderStatus(id++, x)).Where(x => x != null);

            return list;
        }
        private IEnumerable<OrderStatus> GetPredefinedOrderStatus()
        {
            return new List<OrderStatus>()
            {
                OrderStatus.Submitted,
                OrderStatus.AwaitingValidation,
                OrderStatus.StockConfirmed,
                OrderStatus.Paid,
                OrderStatus.Shipped,
                OrderStatus.Cancelled,
            };
        }






        private AsyncRetryPolicy CreatePolicy(ILogger<OrderDBContext> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>()
                         .WaitAndRetryAsync
                         (
                              retryCount: retries,
                              sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                              onRetry: (exception, timeSpan, retry, ctx) =>
                               {
                                   logger.LogWarning(exception, $"[{prefix}] Exception with message");
                               }
                          );
        }






    }
}
