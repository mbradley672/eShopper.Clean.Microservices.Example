using Catalog.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Responses;

public class ProductResponse
{
    [BsonId, BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = string.Empty;
    [BsonElement("Name")]
    public string Name { get; init; } = string.Empty;
    public string Summary { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageFile { get; init; } = string.Empty;
    public ProductBrand Brand { get; init; } = default!;
    public ProductType Type { get; init; } = default!;
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; init; }
}