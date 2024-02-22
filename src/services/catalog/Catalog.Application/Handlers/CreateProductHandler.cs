using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class CreateProductHandler(IProductRepository repository) : IRequestHandler<CreateProductCommand, ProductResponse>
{
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = MapperExtensions.Mapper.Map<Product>(request);
        if (product is null)
        {
            throw new ApplicationException("There was an error mapping the product");
        }
        var newProduct = await repository.CreateProduct(product);
        return MapperExtensions.Mapper.Map<ProductResponse>(newProduct);
    }
}