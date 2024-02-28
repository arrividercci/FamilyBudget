using FamilyBudget.WebServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.WebServer.Data.Repositories
{
    public class FamilyRepository(FamilyBudgetDbContext dbContext) : IFamilyRepository
    {
        public async Task AddMemberAsync(Family family, User member)
        {
            family.Users.Add(member);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddPurchaseAsync(Family family, Purchase purchase)
        {
            family.Purchases.Add(purchase);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Family> CreateFamilyAsync(User user, string familyName)
        {
            var family = new Family()
            {
                Name = familyName,
                Users = new List<User>()
                {
                    user
                },
                Purchases = new List<Purchase>()
            };
            await dbContext.AddAsync(family);

            await dbContext.SaveChangesAsync();

            return family;
        }

        public async Task<Family?> GetByIdAsync(int id)
        {
            var family = await dbContext.Families
                .Include(family => family.Users)
                .Include(family => family.Purchases.OrderByDescending(purchase => purchase.Date))
                .Where(family => family.Id == id)
                .SingleOrDefaultAsync();

            return family;
        }

        public async Task<IEnumerable<Family>> GetByUserIdAsync(string id)
        {
            var user = await dbContext.Users.Include(user => user.Families)
                .SingleOrDefaultAsync(user => user.Id == id);

            return user.Families;
        }

        public async Task RemoveMemberAsync(Family family, string userId)
        {
            var user = family.Users.SingleOrDefault(user => user.Id == userId);
            family.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
