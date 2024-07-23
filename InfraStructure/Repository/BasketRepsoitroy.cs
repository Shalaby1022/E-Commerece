using Core.Interfaces;
using Core.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    public class BasketRepsoitroy : IBasketRepsoitory
    {
        private readonly IDatabase _dataBase;

        public BasketRepsoitroy(IConnectionMultiplexer connectionMultiplexer)
        {
            _dataBase = connectionMultiplexer.GetDatabase();
        } 
        public async Task<bool> DeleteCustomerBasketAsync(string BasketId)
        {
              return await _dataBase.KeyDeleteAsync(BasketId);

        }

        public async Task<CustomerBasket> GetCustomerBasketAsync(string BasketId)
        {
            var data = await _dataBase.StringGetAsync(BasketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);

        }

        public async Task<CustomerBasket> UpdateCustomerBasketAsync(CustomerBasket customerBasket)
        {
            var dataAddedToBasketOrGetUpdated = await _dataBase.StringSetAsync(customerBasket.Id,
                                                                   JsonSerializer.Serialize(customerBasket)
                                                                   , TimeSpan.FromDays(30));

            if (dataAddedToBasketOrGetUpdated == null)
            {
                return null;
            }

            return await GetCustomerBasketAsync(customerBasket.Id);
        }
    }
}
