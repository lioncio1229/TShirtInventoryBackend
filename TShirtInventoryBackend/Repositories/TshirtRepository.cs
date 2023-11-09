using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Repositories
{
    public class TshirtRepository : Repository<Tshirt>, ITshirtRepository
    {
        public TshirtRepository(DbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Tshirt>> GetAll()
        {
            return await context.Set<Tshirt>()
                .Include(tshirt => tshirt.Category)
                .ToListAsync();
        }

        public override async Task<Tshirt> Get(int id)
        {
            return await context.Set<Tshirt>()
                .Include(tshirt => tshirt.Category)
                .FirstOrDefaultAsync(tshirt => tshirt.Id == id);
        }
    }
}
