using System.Linq.Expressions;

namespace TshirtInventoryBackend.Repositories.Common
{
    public interface IRepositoryPKEmail<T> where T : class, IEntityPKEmail
    {
        Task<List<T>> GetAll();
        Task<T> Get(string email);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(string email);
    }
}
