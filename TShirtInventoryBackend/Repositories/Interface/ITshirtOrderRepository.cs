using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories.Interface
{
    public interface ITshirtOrderRepository : IRepository<TshirtOrder>
    {
        public Task<IEnumerable<TshirtOrder>> GetAllWithQuery(string query);
        public Task<IEnumerable<TshirtOrder>> GetAllWithQuery(string query, int statusId);
    }
}
