using LendThingsCommonClasses.Models;

namespace LendThingsAPI.DataAccess
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LendThingsContext context) : base(context)
        {
        }
    }
}