using Catalog.Core.Entities;
using Catalog.Core.Repositories;

namespace Catalog.Infrastructure.Repositories;

public class BrandsRepository : IBrandRepository
{
    public Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        throw new NotImplementedException();
    }
}