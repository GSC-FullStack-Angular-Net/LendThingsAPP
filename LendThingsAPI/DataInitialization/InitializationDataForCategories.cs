using LendThingsAPI.DataAccess;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LendThingsCommonClasses.Models;

namespace LendThingsAPI.DataInitialization
{
    public class InitializationDataForCategories
    {
        async public static Task Initialize(IServiceProvider serviceProvider)
        {

            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            var UoW = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            string[] categories = new string[] { "School", "Tools", "Computer", "Money" };

            var listOfCategories = await UoW.CategoryRepository.GetAllAsync();
            foreach (var description in categories)
            {
                if (!listOfCategories.Any(c => c.Description == description))
                {
                    await UoW.CategoryRepository.AddAsync(new Category() { Description = description });
                }
            }

            UoW.CompleteAsync();
        }

    }
}
