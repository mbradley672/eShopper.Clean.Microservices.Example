using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByNameHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByNameQuery, ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository.GetProduct(request.Name);
        var productResponseList =
            MapperExtensions.Mapper.Map<Product, ProductResponse >(productList);
        return productResponseList;
    }
}