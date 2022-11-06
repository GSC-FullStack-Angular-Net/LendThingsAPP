using LendThingsAPI.Models;

namespace LendThingsAPI.DataAccess
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(LendThingsContext context) : base(context)
        {
        }
    }
}