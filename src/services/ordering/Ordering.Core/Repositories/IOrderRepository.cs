using Ordering.Core.Common;

namespace Ordering.Core.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrderByUserName(string userName);   
}