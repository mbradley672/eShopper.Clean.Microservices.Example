using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class UpdateProductHandler(IProductRepository repository) : IRequestHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.UpdateProduct(new Product()
        {
            Id = request.Id,
            Name = request.Name,
            Summary = request.Summary,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Brand = request.Brand,
            Type = request.Type,
            Price = request.Price
        });
        return product;
    }
}