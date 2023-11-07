using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.RepositoriesV2
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserWithEmail(string email);
        Task<User?> RemoveWithEmail(string email);
    }
}
