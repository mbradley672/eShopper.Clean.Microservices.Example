using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllBrandsAsync();
}

public interface ITypesRepository
{
    Task<IEnumerable<ProductType>> GetAllTypes();
}

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(string id);
    Task<IList<Product>> GetProductsByName(string productName);
    Task<IEnumerable<Product>> GetProductsByBrand(string brandName);
    Task<Product> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}