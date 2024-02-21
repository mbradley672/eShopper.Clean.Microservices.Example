using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public static class TypeContextSeeder
{
    public static async Task SeedData(IMongoCollection<ProductType> productTypeCollection)
    {
        var checkTypes = await productTypeCollection.Find(p => true).AnyAsync();
        var path = Path.Combine("Data", "SeedData", "types.json");
        if (!checkTypes)
        {
            var typesData = await File.ReadAllTextAsync(path);
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            if (types is not null)
            {
                foreach (var type in types)
                {
                    await productTypeCollection.InsertOneAsync(type);
                }
            }
        }
    }
}