using LendThingsCommonClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace LendThingsAPI.DataAccess
{
    public class LoanRepository : BaseRepository<Loan>, ILoanRepository 
    {
        public LoanRepository(LendThingsContext context) : base(context)
        {

        }
        public override Loan GetById(int id)
        {
            return dbSet.Include(l=>l.Thing).Include(l=>l.Person).SingleOrDefault(l=>l.Id == id);
        }
    }
}