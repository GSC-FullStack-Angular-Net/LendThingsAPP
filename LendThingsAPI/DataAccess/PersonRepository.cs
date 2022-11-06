using LendThingsAPI.Models;

namespace LendThingsAPI.DataAccess
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(LendThingsContext context) : base(context)
        {
        }
    }
}