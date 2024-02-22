using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Commands;

public class CreateProductCommand : IRequest<ProductResponse>
{ 
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