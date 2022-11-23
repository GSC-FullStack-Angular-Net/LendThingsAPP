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

        public TEntity Add(TEntity entity)
        {
            var savedEntity = dbSet.Add(entity);
            return savedEntity.Entity;
        }

        public bool Delete(int id)
        {
            TEntity? result = dbSet.SingleOrDefault(x => x.Id == id);
            if (result !=null)
            {
                dbSet.Remove(result);
                return true;
            }
            return false;
        }

        public virtual List<TEntity> GetAll()
        {
            return dbSet.ToList(); 
        }

        public virtual TEntity GetById(int id)
        {
            return dbSet.SingleOrDefault(t=>t.Id == id);
        }

        public TEntity Update(TEntity entity)
        {
            return dbSet.Update(entity).Entity;
        }
    }
}
