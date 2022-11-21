using LendThingsCommonClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace LendThingsAPI.DataAccess
{
    public class ThingRepository : BaseRepository<Thing>
    {
        public ThingRepository(LendThingsContext context) : base(context)
        {
        }

        public override List<Thing> GetAll()
        {
            return dbSet.Include(t=>t.Category).ToList();
        }

        public override Thing GetById(int id)
        {
            return dbSet.Include(t => t.Category).SingleOrDefault(t=>t.Id==id);
        }
    }
}