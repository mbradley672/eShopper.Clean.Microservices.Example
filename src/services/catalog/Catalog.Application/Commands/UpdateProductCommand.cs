using Catalog.Core.Entities;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Commands;

public class UpdateProductCommand : IRequest<bool>
{
    [BsonId, BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [BsonElement("Name")]
    public string Name { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageFile { get; set; } = string.Empty;
    public ProductBrand Brand { get; set; } = default!;
    public ProductType Type { get; set; } = default!;
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
}