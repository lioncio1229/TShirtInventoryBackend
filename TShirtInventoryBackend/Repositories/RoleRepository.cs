using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}
