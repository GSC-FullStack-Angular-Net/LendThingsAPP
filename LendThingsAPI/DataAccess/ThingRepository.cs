using LendThingsCommonClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace LendThingsAPI.DataAccess
{
    public class ThingRepository : BaseRepository<Thing>, IThingRepository
    {
        public ThingRepository(LendThingsContext context) : base(context)
        {
        }

        async public override Task<List<Thing>> GetAllAsync()
        {
            return await dbSet.Include(t => t.Category).ToListAsync();
        }

        async public override Task<Thing> GetByIdAsync(int id)
        {
            return await dbSet.Include(t => t.Category).SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}