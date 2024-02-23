using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Core.Repositories;

public interface IProductRepository
{
    Task<Pagination<Product>> GetProducts(CatalogSpecificationParams specificationParams);
    Task<Product> GetProduct(string id);
    Task<IList<Product>> GetProductsByName(string productName);
    Task<IEnumerable<Product>> GetProductsByBrand(string brandName);
    Task<Product> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}