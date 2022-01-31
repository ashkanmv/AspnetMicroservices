using Catalog.Api.Data.Abstraction;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data.Implementation
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.Seed(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
