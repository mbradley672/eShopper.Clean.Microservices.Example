using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public static class BrandContextSeeder
{
    public static async Task SeedData(IMongoCollection<ProductBrand> productBrandCollection)
    {
        var checkBrands = await productBrandCollection.Find(p => true).AnyAsync();
        var path = Path.Combine("Data", "SeedData", "brands.json");
        if (!checkBrands)
        {
            var brandsData = await File.ReadAllTextAsync(path);
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (brands is not null)
            {
                foreach (var brand in brands)
                {
                    await productBrandCollection.InsertOneAsync(brand);
                }
            }
        }
    }
}