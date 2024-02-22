using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByNameHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
{
    public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository.GetProductsByName(request.Name);
        var productResponseList =
            MapperExtensions.Mapper.Map<IList<ProductResponse>>(productList);
        return productResponseList;
    }
}