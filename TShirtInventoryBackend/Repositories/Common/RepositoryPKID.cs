using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TshirtInventoryBackend.Data;

namespace TshirtInventoryBackend.Repositories.Common
{
    public abstract class RepositoryPKID<TEntity, TContext> : IRepositoryPKID<TEntity>
        where TEntity : class, IEntityPKID
        where TContext : DataContext
    {
        private readonly TContext context;
        public RepositoryPKID(TContext context)
        {
            this.context = context;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Delete(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
