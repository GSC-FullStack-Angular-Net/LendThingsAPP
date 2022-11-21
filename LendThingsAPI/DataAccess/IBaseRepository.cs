using LendThingsCommonClasses.Models;

namespace LendThingsAPI.DataAccess
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Add(TEntity entity);
        bool Delete(int id);
        List<TEntity> GetAll();
        TEntity GetById(int id);
        TEntity Update(TEntity entity);
    }
}