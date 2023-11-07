using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepositories { get; }
        IRoleRepository RoleRepositories { get; }

        Task<User> AddNewUser(UserAddInputs userInput);

        Task<User?> UpdateUser(string userEmail, UserUpdateInputs userInput);

        string? Authenticate(string email, string password);

        int Complete();
    }
}
