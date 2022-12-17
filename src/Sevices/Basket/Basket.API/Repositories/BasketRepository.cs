using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCach;

        public BasketRepository(IDistributedCache redisCach)
        {
            _redisCach = redisCach;
        }


        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await  _redisCach.GetStringAsync(username);
            if (string.IsNullOrWhiteSpace(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCach.SetStringAsync(basket.UserName,JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string username)
        {
             await _redisCach.RemoveAsync(username);
        }
    }
}
