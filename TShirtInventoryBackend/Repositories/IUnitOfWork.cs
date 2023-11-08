using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Request;

namespace TshirtInventoryBackend.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IBlacklistedTokenRepository BlacklistedTokenRepository { get; }
        ITshirtRepository TshirtRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task<User> AddNewUser(UserAddInputs userInput);

        Task<User?> UpdateUser(string userEmail, UserUpdateInputs userInput);

        string GenerateToken(string email, string password);

        void BlacklistToken(string token);

        public bool IsTokenValid(string jti);

        Task<Tshirt> AddTshirt(TshirtRequest tshirt);
        Task UpdateTshirt(int id, TshirtRequest tshirtRequest);
        Task<Tshirt?> RemoveTshirt(int id);
    }
}
