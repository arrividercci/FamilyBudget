using FamilyBudget.WebServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.WebServer.Data.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUserAsync(string id);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task CreatePurchaseAsync(User user, Purchase purchase);
        public Task RemovePurchase(User user, int purchaseId);
    }
}
