using LendThingsAPI.Models;

namespace LendThingsAPI.DataAccess
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        LoanRepository LoanRepository { get; }
        PersonRepository PersonRepository { get; }
        ThingRepository ThingRepository { get; }

        int CompleteAsync();
    }
}