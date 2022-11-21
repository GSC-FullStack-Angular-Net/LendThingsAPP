
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
        Task<List<B>> GetAllBaseAsync();

        Task<List<F>> GetAllFullAsync();

        Task<F> GetByIdAsync(int id);


        Task SaveAsync(C entity);
        Task UpdateAsync(U entity);

        bool Exists(int id);

        Task DeleteAsync(D entity);

    }
}
