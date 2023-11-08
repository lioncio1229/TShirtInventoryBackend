using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepositories { get; }
        IRoleRepository RoleRepositories { get; }
        IBlacklistedTokenRepository BlacklistedTokenRepositories { get; }

        Task<User> AddNewUser(UserAddInputs userInput);

        Task<User?> UpdateUser(string userEmail, UserUpdateInputs userInput);

        string GenerateToken(string email, string password);

        void BlacklistToken(string token);

        public bool IsTokenValid(string jti);

        int Complete();
    }
}
