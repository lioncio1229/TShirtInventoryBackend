using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserWithEmail(string email);
        Task<User?> RemoveWithEmail(string email);
    }
}
