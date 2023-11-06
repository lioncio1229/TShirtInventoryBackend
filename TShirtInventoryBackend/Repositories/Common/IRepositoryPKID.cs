using System.Linq.Expressions;

namespace TshirtInventoryBackend.Repositories.Common
{
    public interface IRepositoryPKID<T> where T : class, IEntityPKID
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
