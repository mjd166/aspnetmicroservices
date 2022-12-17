using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProdutRepository : IProductRepository
    {
        private readonly ICatalogContext catalogContext;

        public ProdutRepository(ICatalogContext catalogContext)
        {
            this.catalogContext = catalogContext;
        }



        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await catalogContext.Products
                                   .Find(p => true)
                                   .ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await catalogContext.Products.Find(filter).ToListAsync();

        }

        public async  Task Create(Product product)
        {
             await catalogContext.Products.InsertOneAsync(product);
        }
        public async Task<bool> Update(Product product)
        {
            var updateResult =await catalogContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged 
                   && updateResult.ModifiedCount >0;
        }
        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult =await  catalogContext.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

    }
}
