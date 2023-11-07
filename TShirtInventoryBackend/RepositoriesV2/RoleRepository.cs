using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.RepositoriesV2
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}
