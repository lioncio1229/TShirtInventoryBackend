using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await context.Set<Order>()
                .Include(order => order.Customer)
                .Include(order => order.Status)
                .Include(order => order.TshirtOrders)
                    .ThenInclude(to => to.Tshirt)
                        .ThenInclude(tshirt => tshirt.Category)
                .ToListAsync();
        }
    }
}
