using LendThingsCommonClasses.Models;

namespace LendThingsAPI.DataAccess
{
    public class LoanRepository : BaseRepository<Loan>, ILoanRepository 
    {
        public LoanRepository(LendThingsContext context) : base(context)
        {
        }
    }
}