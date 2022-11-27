using LendThingsCommonClasses.Models;

namespace LendThingsAPI.DataAccess
{
    public interface IThingRepository:IBaseRepository<Thing>
    {
        Task<List<Thing>> GetAllAsync();
        Task<Thing> GetByIdAsync(int id);
    }
}