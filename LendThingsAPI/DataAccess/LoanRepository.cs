using LendThingsCommonClasses.Models;

namespace LendThingsAPI.DataAccess
{
    public class LoanRepository : BaseRepository<Loan>
    {
        public LoanRepository(LendThingsContext context) : base(context)
        {
        }
    }
}