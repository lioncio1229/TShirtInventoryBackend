using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Data;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories.Common;

namespace TshirtInventoryBackend.Repositories
{
    public class UserRepository : RepositoryPKEmail<User, DataContext>
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public override async Task<User> Get(string email)
        {
            return await context.Set<User>()
                .Include(o => o.Role)
                .FirstOrDefaultAsync(o => o.Email == email);
        }

        public async override Task<List<User>> GetAll()
        {
            return await context.Set<User>()
                .Include(o => o.Role)
                .ToListAsync();
        }
    }
}
