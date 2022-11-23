using LendThingsCommonClasses.Models;

namespace LendThingsAPI.DataAccess
{
    public interface IThingRepository:IBaseRepository<Thing>
    {
        List<Thing> GetAll();
        Thing GetById(int id);
    }
}