using LendThingsCommonClasses.Models;


namespace LendThingsAPI.DataAccess
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        LoanRepository LoanRepository { get; }
        IPersonRepository PersonRepository { get; }
        ThingRepository ThingRepository { get; }

        int CompleteAsync();
    }
}