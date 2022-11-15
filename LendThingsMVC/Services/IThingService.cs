using LendThingsAPI.Models;

namespace LendThingsMVC.Services
{
    public interface IThingService
    {
        List<Thing> GetAll(string search);
        Task<List<Thing>> GetAllAsync();

        Thing GetById(int id);
        Task<Thing> GetByIdAsync(int id);


        void SaveAsync(Thing alumno);
        void UpdateAsync(Thing alumno);

        bool Exists(int id);

        void Delete(Thing alumno);
        void DeleteAsync(Thing alumno);

    }
}
