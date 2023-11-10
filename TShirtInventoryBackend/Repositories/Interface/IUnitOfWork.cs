using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Request;

namespace TshirtInventoryBackend.Repositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IBlacklistedTokenRepository BlacklistedTokenRepository { get; }
        ITshirtRepository TshirtRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IOrderRepository OrderRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ITshirtOrderRepository TshirtOrderRepository { get; }
        IStatusRepository StatusRepository { get; }

        Task<User> AddNewUser(UserAddInputs userInput);

        Task<User?> UpdateUser(string userEmail, UserUpdateInputs userInput);

        string GenerateToken(string email, string password);

        void BlacklistToken(string token);

        public bool IsTokenValid(string jti);

        Task<Tshirt> AddTshirt(TshirtRequest tshirt);
        Task UpdateTshirt(int id, TshirtRequest tshirtRequest);
        Task<Tshirt?> RemoveTshirt(int id);
        Task<Order?> CreateOrder(int customerId, OrderRequest orderRequest);
        Task<bool> UpdateOrderStatus(int orderId, int statusId);
    }
}
