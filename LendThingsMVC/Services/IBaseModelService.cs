
using LendThingsMVC.Models;

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
         Task<ProcesedResponse<string>> DeleteAsync(D entity);
         bool Exists(int id);
         Task<ProcesedResponse<List<B>>> GetAllBaseAsync();
         Task<ProcesedResponse<List<F>>> GetAllFullAsync();
         Task<ProcesedResponse<F>> GetByIdAsync(int id);
         Task<ProcesedResponse<B>> SaveAsync(C entity);
         Task<ProcesedResponse<B>> UpdateAsync(U entity);

    }
}
