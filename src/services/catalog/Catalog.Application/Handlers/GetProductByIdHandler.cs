using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByIdHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository.GetProduct(request.Id);
        var productResponseList =
            MapperExtensions.Mapper.Map<Product, ProductResponse >(productList);
        return productResponseList;
    }
}