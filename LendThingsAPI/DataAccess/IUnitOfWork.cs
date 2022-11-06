using LendThingsAPI.Models;

namespace LendThingsAPI.DataAccess
{
    public interface IUnitOfWork
    {
        CategoryRepository CategoryRepository { get; }
        LoanRepository LoanRepository { get; }
        PersonRepository PersonRepository { get; }
        ThingRepository ThingRepository { get; }

        int CompleteAsync();
    }
}