using Ordering.Core.Common;

namespace Ordering.Core.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Entities.Order>> GetOrderByUserName(string userName);   
}