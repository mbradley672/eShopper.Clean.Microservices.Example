using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public static class ProductsContextSeeder
{
    public static async Task SeedData(IMongoCollection<Product> productCollection)
    {
        var checkProducts = await productCollection.Find(p => true).AnyAsync();
        var path = Path.Combine("Data", "SeedData", "products.json");
        if (!checkProducts)
        {
            var productData = await File.ReadAllTextAsync("../Catalog.Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productData);
            if (products is not null)
            {
                foreach (var product in products)
                {
                    await productCollection.InsertOneAsync(product);
                }
            }
        }
    }
}