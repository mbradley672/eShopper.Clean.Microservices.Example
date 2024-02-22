using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllProductsByBrandHandler(IProductRepository productRepository)
    : IRequestHandler<GetAllProductsByBrandQuery, IList<ProductResponse>>
{
    public async Task<IList<ProductResponse>> Handle(GetAllProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository.GetProductsByBrand(request.BrandName);
        var productResponseList =
            MapperExtensions.Mapper.Map<IList<Product>, IList<ProductResponse>>(productList.ToList());
        return productResponseList;
    }
}