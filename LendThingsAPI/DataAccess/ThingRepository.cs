using LendThingsAPI.Models;

namespace LendThingsAPI.DataAccess
{
    public class ThingRepository : BaseRepository<Thing>
    {
        public ThingRepository(LendThingsContext context) : base(context)
        {
        }
    }
}