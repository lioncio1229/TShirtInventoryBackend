using TshirtInventoryBackend.Data;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.RepositoriesV2
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
            UserRepositories = new UserRepository(_context);
            RoleRepositories = new RoleRepository(_context);
        }

        public IUserRepository UserRepositories { get; private set; }
        public IRoleRepository RoleRepositories { get; private set; }

        public async Task<User> AddNewUser(UserRegistrationInputs userInput)
        {
            var role = await RoleRepositories.Get(userInput.RoleId);
            var newUser = new User
            {
                Email = userInput.Email,
                Password = userInput.Password,
                FullName = userInput.FullName,
                Role = role,
                IsActived = true,
            };
            UserRepositories.Add(newUser);
            Complete();

            return newUser;
        }

        public async Task<User?> UpdateUser(string userEmail, UserUpdateInputs userInput)
        {
            var role = await RoleRepositories.Get(userInput.RoleId);
            var user = await UserRepositories.GetUserWithEmail(userEmail);

            if(user == null)
            {
                return null;
            }

            user.FullName = userInput.FullName;
            user.Role = role;
            user.IsActived = userInput.IsActive;
            Complete();

            return user;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
