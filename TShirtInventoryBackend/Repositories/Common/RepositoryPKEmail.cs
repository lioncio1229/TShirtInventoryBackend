using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Data;

namespace TshirtInventoryBackend.Repositories.Common
{
    public abstract class RepositoryPKEmail<TEntity, TContext> : IRepositoryPKEmail<TEntity>
        where TEntity : class, IEntityPKEmail
        where TContext : DataContext
    {
        private readonly TContext context;
        public RepositoryPKEmail(TContext context)
        {
            this.context = context;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Delete(string email)
        {
            var entity = await context.Set<TEntity>().FindAsync(email);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> Get(string email)
        {
            return await context.Set<TEntity>().FindAsync(email);
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
