using LendThingsCommonClasses.Models;

namespace LendThingsAPI.DataAccess
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(LendThingsContext context) : base(context)
        {
        }
    }
}