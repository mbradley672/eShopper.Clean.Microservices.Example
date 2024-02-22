using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllTypesHandler(ITypesRepository typesRepository)
    : IRequestHandler<GetAllTypesQuery, IList<TypeResponse>>
{
    public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var allTypes = await typesRepository.GetAllTypes();
        var responseList = MapperExtensions.Mapper.Map<IList<ProductType>, IList<TypeResponse>>(allTypes.ToList());
        return responseList;
    }
}