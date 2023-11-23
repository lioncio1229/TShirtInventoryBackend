using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Repositories.Interface
{
    public interface ITshirtRepository : IRepository<Tshirt>
    {
        Task<IEnumerable<Tshirt>> GetWithQuery(int startPosition, int numberOfItems, string searchByName="");
        int GetTotalProducts();
    }
}
