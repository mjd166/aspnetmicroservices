using Basket.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Basket.API.Data
{
    public interface IShoppingCartContext
    {
        IMongoCollection<ShoppingCart> ShoppingCarts { get; }
    }

    public class ShoppingCartContext : IShoppingCartContext
    {
        private readonly IConfiguration _configuration;

        public ShoppingCartContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration.GetValue<string>("DatabaseSettings:Connectionstring"));
            var database = client.GetDatabase(_configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            ShoppingCarts = database.GetCollection<ShoppingCart>(_configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }

        public IMongoCollection<ShoppingCart> ShoppingCarts { get; }
    }
}


