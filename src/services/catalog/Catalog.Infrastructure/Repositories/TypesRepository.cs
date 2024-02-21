using Catalog.Core.Entities;
using Catalog.Core.Repositories;

namespace Catalog.Infrastructure.Repositories;

public class TypesRepository : ITypesRepository
{
    public Task<IEnumerable<ProductType>> GetAllTypes()
    {
        throw new NotImplementedException();
    }
}