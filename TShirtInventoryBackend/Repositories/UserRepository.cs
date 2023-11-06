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
    }
}
