using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using Catalog.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogContext context) : IProductRepository, ITypesRepository, IBrandRepository
{
    public async Task<Pagination<Product>> GetProducts(CatalogSpecificationParams specificationParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;
        if (!string.IsNullOrEmpty(specificationParams.Search))
        {
            var searchFilter = builder.Regex(x => x.Name, new BsonRegularExpression(specificationParams.Search));
            filter &= searchFilter;
        }

        if (!string.IsNullOrEmpty(specificationParams.BrandId))
        {
            var brandFilter = builder.Eq(x => x.Brand.Id, specificationParams.BrandId);
            filter &= brandFilter;
        }

        if (!string.IsNullOrEmpty(specificationParams.TypeId))
        {
            var typeFilter = builder.Eq(x => x.Type.Id, specificationParams.TypeId);
            filter &= typeFilter;
        }

        if (!string.IsNullOrEmpty(specificationParams.Sort))
        {
            return new Pagination<Product>()
            {
                PageSize = specificationParams.PageSize,
                PageIndex = specificationParams.PageIndex,
                Count = await context.Products.CountDocumentsAsync(p => true),
                Data = await DataFilter(specificationParams, filter),
            };
        }

        return new Pagination<Product>()
        {
            PageSize = specificationParams.PageSize,
            PageIndex = specificationParams.PageIndex,
            Count = await context.Products.CountDocumentsAsync(p => true),
            Data = await context.Products.Find(filter)
                .Sort(Builders<Product>.Sort.Ascending("Name"))
                .Skip((specificationParams.PageIndex - 1) * specificationParams.PageSize)
                .Limit(specificationParams.PageSize)
                .ToListAsync()
        };
    }

    private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecificationParams specificationParams,
        FilterDefinition<Product> filter)
    {
        return specificationParams.Sort.ToLower() switch
        {
            "priceasc" => await context.Products.Find(filter)
                .Sort(Builders<Product>.Sort.Ascending("Price"))
                .Skip((specificationParams.PageIndex - 1) * specificationParams.PageSize)
                .Limit(specificationParams.PageSize)
                .ToListAsync(),
            "pricedesc" => await context.Products.Find(filter)
                .Sort(Builders<Product>.Sort.Descending("Price"))
                .Skip((specificationParams.PageIndex - 1) * specificationParams.PageSize)
                .Limit(specificationParams.PageSize)
                .ToListAsync(),
            _ => await context.Products.Find(filter)
                .Sort(Builders<Product>.Sort.Ascending("Name"))
                .Skip((specificationParams.PageIndex - 1) * specificationParams.PageSize)
                .Limit(specificationParams.PageSize)
                .ToListAsync()
        };
    }

    public async Task<Product> GetProduct(string id)
    {
        return await context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IList<Product>> GetProductsByName(string productName)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, productName);
        return await context.Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string brandName)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Brand.Name, brandName);
        return await context.Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await context.Products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await context.Products
            .ReplaceOneAsync(p => p.Id == product.Id, product);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var deleteResult = await context.Products
            .DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        return await context.ProductTypes.Find(p => true).ToListAsync();
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
    {
        return await context.ProductBrands.Find(p => true).ToListAsync();
    }
}