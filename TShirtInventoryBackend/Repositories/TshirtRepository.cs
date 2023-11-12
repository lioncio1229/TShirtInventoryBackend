using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories.Interface;

namespace TshirtInventoryBackend.Repositories
{
    public class TshirtRepository : Repository<Tshirt>, ITshirtRepository
    {
        public TshirtRepository(DbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Tshirt>> GetAllAsync()
        {
            return await context.Set<Tshirt>()
                .Include(tshirt => tshirt.Category)
                .ToListAsync();
        }

        public override Tshirt Get(int id)
        {
            return context.Set<Tshirt>()
               .Include(tshirt => tshirt.Category)
               .FirstOrDefault(tshirt => tshirt.Id == id);
        }

        public override async Task<Tshirt> GetAsync(int id)
        {
            return await context.Set<Tshirt>()
                .Include(tshirt => tshirt.Category)
                .FirstOrDefaultAsync(tshirt => tshirt.Id == id);
        }

        public async Task<IEnumerable<Tshirt>> GetWithQuery(int skipRows, int numberOfItems)
        {
            return await context.Set<Tshirt>()
                .Include(tshirt => tshirt.Category)
                .Skip(skipRows)
                .Take(numberOfItems)
                .ToListAsync();
        }

        public int GetTotalCount()
        {
            return context.Set<Tshirt>().Count();
        }
    }
}
