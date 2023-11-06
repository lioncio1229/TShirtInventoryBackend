using TshirtInventoryBackend.Data;
using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Repositories.Common;

namespace TshirtInventoryBackend.Repositories
{
    public class RoleRepository : RepositoryPKID<Role, DataContext>
    {
        public RoleRepository(DataContext context) : base(context)
        {
        }
    }
}
