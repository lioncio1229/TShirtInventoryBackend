using TshirtInventoryBackend.Models;
using TshirtInventoryBackend.Models.Reponse;

namespace TshirtInventoryBackend.Repositories.Interface
{
    public interface ITshirtOrderRepository : IRepository<TshirtOrder>
    {
        public Task<IEnumerable<TshirtOrder>> GetAllWithQuery(string query);
        public Task<IEnumerable<TshirtOrder>> GetAllWithQuery(string query, int statusId);
        public SaleSummeryResponse GetSaleSummary();
    }
}
