using LendThingsCommonClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace LendThingsAPI.DataAccess
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected LendThingsContext context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(LendThingsContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        async public Task<TEntity> AddAsync(TEntity entity)
        {
            var savedEntity = await dbSet.AddAsync(entity);
            return savedEntity.Entity;
        }

        async public Task<bool> DeleteAsync(int id)
        {
            TEntity? result = await dbSet.SingleOrDefaultAsync(x => x.Id == id);
            if (result !=null)
            {
                dbSet.Remove(result);
                return true;
            }
            return false;
        }

        async public virtual Task<List<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync(); 
        }

        async public virtual Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.SingleOrDefaultAsync(t => t.Id == id);
        }

        public TEntity Update(TEntity entity)
        {
            return dbSet.Update(entity).Entity;
        }
    }
}
