
namespace LendThingsMVC.Services
{
    /// <summary>
    /// </summary>
    /// <typeparam name="B">BaseDTO</typeparam>
    /// <typeparam name="F">FullDTO</typeparam>
    /// <typeparam name="C">CreationDTO</typeparam>
    /// <typeparam name="U">UpdateDTO</typeparam>
    /// <typeparam name="D">DeletionDTO</typeparam>
    public interface IBaseModelService<B,F,C,U,D>
    {
        List<B> GetAllBase();
        Task<List<B>> GetAllBaseAsync();

        List<F> GetAllFull();
        Task<List<F>> GetAllFullAsync();

        F GetById(int id);
        Task<F> GetByIdAsync(int id);


        void SaveAsync(C entity);
        void UpdateAsync(U entity);

        bool Exists(int id);

        void Delete(D entity);
        void DeleteAsync(D entity);

    }
}
