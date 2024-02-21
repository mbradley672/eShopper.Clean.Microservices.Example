using Catalog.Core.Entities;
using Catalog.Core.Repositories;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<IEnumerable<Product>> GetProducts()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProduct(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsByName(string productName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsByBrand(string brandName)
    {
        throw new NotImplementedException();
    }

    public Task<Product> CreateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }
}