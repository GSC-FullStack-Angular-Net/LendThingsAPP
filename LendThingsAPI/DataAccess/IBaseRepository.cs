using LendThingsCommonClasses.Models;

namespace LendThingsAPI.DataAccess
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        TEntity Update(TEntity entity);
    }
}