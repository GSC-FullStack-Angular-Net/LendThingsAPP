using LendThingsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LendThingsAPI.DataAccess
{
    public class LendThingsContext:DbContext
    {
        
        public LendThingsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=LendThingsDB;Trusted_Connection=True;");
        }

        public DbSet<Thing> Things { get; set; }
        public DbSet<Loan>  Loans { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
