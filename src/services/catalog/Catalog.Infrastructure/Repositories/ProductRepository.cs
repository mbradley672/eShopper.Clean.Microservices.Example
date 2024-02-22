﻿using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogContext context) : IProductRepository, ITypesRepository, IBrandRepository
{
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await context.Products.Find(p=> true).ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
        return await context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string productName)
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
            .ReplaceOneAsync(p=> p.Id == product.Id, product);
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
        return await context.ProductTypes.Find(p=> true).ToListAsync();
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
    {
        return await context.ProductBrands.Find(p=> true).ToListAsync();
    }
}