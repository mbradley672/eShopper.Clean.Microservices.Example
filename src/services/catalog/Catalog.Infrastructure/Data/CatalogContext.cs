using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;

namespace Catalog.Infrastructure.Data;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<ProductBrand> ProductBrands { get; }
    public IMongoCollection<ProductType> ProductTypes { get; }

    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:ProductsCollectionName"));
        ProductBrands = database.GetCollection<ProductBrand>(configuration.GetValue<string>("DatabaseSettings:ProductBrandsCollectionName"));
        ProductTypes = database.GetCollection<ProductType>(configuration.GetValue<string>("DatabaseSettings:ProductTypesCollectionName"));

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        ProductsContextSeeder.SeedData(Products);
        BrandContextSeeder.SeedData(ProductBrands);
        TypeContextSeeder.SeedData(ProductTypes);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }
}