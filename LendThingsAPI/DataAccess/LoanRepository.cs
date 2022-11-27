using LendThingsCommonClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace LendThingsAPI.DataAccess
{
    public class LoanRepository : BaseRepository<Loan>, ILoanRepository 
    {
        public LoanRepository(LendThingsContext context) : base(context)
        {

        }
        async public override Task<List<Loan>> GetAllAsync()
        {
            return await dbSet.Include(l => l.Thing.Category).Include(l => l.Person).ToListAsync();
        }

        async public override Task<Loan> GetByIdAsync(int id)
        {
            return await dbSet.Include(l=>l.Thing).Include(l=>l.Person).SingleOrDefaultAsync(l=>l.Id == id);
        }
    }
}