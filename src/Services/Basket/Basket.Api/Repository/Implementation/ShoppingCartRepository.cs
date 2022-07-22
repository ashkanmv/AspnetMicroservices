using Basket.Api.Entities;
using Basket.Api.Repository.Abstraction;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repository.Implementation
{
    public class ShoppingCartRepository : IShoppingRepository
    {
        private readonly IDistributedCache _redisCache;

        public ShoppingCartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart?> GetShoppingCartAsync(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (basket == null)
                return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart cart)
        {
            await _redisCache.SetStringAsync(cart.UserName,JsonConvert.SerializeObject(cart));

            return await GetShoppingCartAsync(cart.UserName);
        }

        public async Task DeleteShoppingCartAsync(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
