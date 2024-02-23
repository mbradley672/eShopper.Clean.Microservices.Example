using Catalog.Application.Responses;
using Catalog.Core.Specifications;
using MediatR;

namespace Catalog.Application.Queries;

public class GetAllProductsQuery : IRequest<Pagination<ProductResponse>>
{
    public CatalogSpecificationParams SpecificationParams { get; set; }

    public GetAllProductsQuery(CatalogSpecificationParams specificationParams)
    {
        SpecificationParams = specificationParams;
    }
}