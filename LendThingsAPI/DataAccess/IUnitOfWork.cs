using LendThingsCommonClasses.Models;


namespace LendThingsAPI.DataAccess
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        ILoanRepository LoanRepository { get; }
        IPersonRepository PersonRepository { get; }
        IThingRepository ThingRepository { get; }

        int CompleteAsync();
    }
}