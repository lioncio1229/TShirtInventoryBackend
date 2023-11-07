using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.RepositoriesV2
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepositories { get; }
        IRoleRepository RoleRepositories { get; }

        Task<User> AddNewUser(UserRegistrationInputs userInput);

        Task<User?> UpdateUser(string userEmail, UserUpdateInputs userInput);

        int Complete();
    }
}
