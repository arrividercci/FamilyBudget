using FamilyBudget.WebServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.WebServer.Data.Repositories
{
    public class UserRepository(FamilyBudgetDbContext dbContext) : IUserRepository
    {
        public async Task CreatePurchaseAsync(User user, Purchase purchase)
        {
            user.Purchases.Add(purchase);
            await dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserAsync(string id)
        {
            return await dbContext.Users
                .Where(user => user.Id == id)
                .Include(user => user.Purchases.OrderByDescending(purchase => purchase.Date))
                .SingleOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(user => user.Email == email);

            return user;
        }
    }
}
