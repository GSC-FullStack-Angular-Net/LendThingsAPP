using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LendThingsAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LendThingsAPI.Models
{
    public class SampleData
    {
        async public static Task Initialize(IServiceProvider serviceProvider)
        {

            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<LendThingsContext>();


            string[] roles = new string[] { "Owner", "Administrator", "Subscriber" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.NormalizedName == role.ToUpper()))
                {
                    await roleStore.CreateAsync(new IdentityRole(role) { NormalizedName= role.ToUpper() });
                }
            }


            var user = new User
            {
                FirstName = "Martin",
                LastName = "Lambrecht",
                Email = "martinoscarlambrecht@gmail.com",
                NormalizedEmail = "MARTINOSCARLAMBRECHT@GMAIL.COM",
                UserName = "MartinLambrecht",
                NormalizedUserName = "MARTINLAMBRECHT",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;

                var userStore = new UserStore<User>(context);
                var result = userStore.CreateAsync(user);

            }

            await AssignRoles(scope, user.Email, roles);

            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceScope scope, string email, string[] roles)
        {

            UserManager<User> _userManager = scope.ServiceProvider.GetService<UserManager<User>>()!;
            User user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }

    }
}
