using FamilyBudget.WebServer.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.WebServer.Data
{
    public class FamilyBudgetDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        public DbSet<Family> Families { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
