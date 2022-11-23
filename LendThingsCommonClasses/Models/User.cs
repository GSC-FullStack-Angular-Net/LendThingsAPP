using Microsoft.AspNetCore.Identity;

namespace LendThingsCommonClasses.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
