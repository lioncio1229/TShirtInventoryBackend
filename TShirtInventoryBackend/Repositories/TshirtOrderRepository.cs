using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Repositories
{
    public class TshirtOrderRepository : Repository<TshirtOrder>, ITshirtOrderRepository
    {
        public TshirtOrderRepository(DbContext context) : base(context)
        {
        }
    }
}
