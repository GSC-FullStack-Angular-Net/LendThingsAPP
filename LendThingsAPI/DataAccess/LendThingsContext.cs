using LendThingsAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LendThingsAPI.DataAccess
{
    public class LendThingsContext:IdentityDbContext<User>
    {
        
        public LendThingsContext(DbContextOptions<LendThingsContext> options) : base(options)
        {
        }

        public DbSet<Thing> Things { get; set; }
        public DbSet<Loan>  Loans { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
