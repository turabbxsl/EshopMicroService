using BasketService.API.Core.Application.Repository;
using BasketService.API.Core.Domain.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.API.Infrastructure.Repository
{
    public class RedisSebetRepository : ISebetRepository
    {

        private readonly IDatabase _database; // <--- DBContext evezi

        private readonly ILogger<RedisSebetRepository> _logger;
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisSebetRepository(ILoggerFactory loggerFactory, ConnectionMultiplexer connectionMultiplexer)
        {
            _logger = loggerFactory.CreateLogger<RedisSebetRepository>();
            _connectionMultiplexer = connectionMultiplexer;
            _database = _connectionMultiplexer.GetDatabase();
        }




        public async Task<bool> DeleteSebetAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }


        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys(); // <--- Key-lerimiz istifadeci adi idi ele

            return data?.Select(k => k.ToString()); // <--- Bu istifadecileri de string-e cevirerek geriye donuruk
        }


        public async Task<Sebet> GetSebetAsync(string customerId)
        {
            var data = await _database.StringGetAsync(customerId);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Sebet>(data);
        }



        public async Task<Sebet> UpdateSebetAsync(Sebet sebet)
        {
            // Bize gelen sebetin BuyId-si ile Sebeti Set edirik
            var created = await _database.StringSetAsync(sebet.BuyId, JsonConvert.SerializeObject(sebet));
            //BuyId var ise Onu Set etdik,yoxdursa yenisini yaradirig

            if (!created)
            {
                _logger.LogInformation("Problem occur persisting the item");
            }

            _logger.LogInformation("Sebet item persisted succesfully");

            return await GetSebetAsync(sebet.BuyId);
        }




        private IServer GetServer()
        {
            var endpoint = _connectionMultiplexer.GetEndPoints();
            return _connectionMultiplexer.GetServer(endpoint.First()); // <--- Tapdigi ilk endpointdeki Serveri geriye donderecek bize
        }

    }
}
