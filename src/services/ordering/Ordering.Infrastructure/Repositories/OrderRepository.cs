using Microsoft.EntityFrameworkCore;
using Ordering.Core.Common;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(OrderContext context) : BaseRepository<Order>(context), IOrderRepository
{
    private readonly OrderContext _context = context;

    public async Task<List<Core.Entities.Order>> GetOrderByUserName(string userName)
    {
        var orderList = await _context.Orders
            .Where(o => o.UserName == userName)
            .ToListAsync();
        return orderList;
    }
}