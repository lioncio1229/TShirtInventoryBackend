using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepositories { get; }
        IRoleRepository RoleRepositories { get; }

        Task<User> AddNewUser(UserRegistrationInputs userInput);

        Task<User?> UpdateUser(string userEmail, UserUpdateInputs userInput);

        string? Authenticate(string email, string password);

        int Complete();
    }
}
