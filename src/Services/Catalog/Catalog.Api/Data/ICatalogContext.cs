using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data.Abstraction
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
