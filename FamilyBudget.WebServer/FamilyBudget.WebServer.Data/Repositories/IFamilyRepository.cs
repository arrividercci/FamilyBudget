using FamilyBudget.WebServer.Data.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.WebServer.Data.Repositories
{
    public interface IFamilyRepository
    {
        public Task<IEnumerable<Family>> GetByUserIdAsync(string id);
        public Task<Family?> GetByIdAsync(int id);
        public Task<Family> CreateFamilyAsync(User user, string familyName);
        public Task AddMemberAsync(Family family, User member);
        public Task RemoveMemberAsync(Family family, string userId);
        public Task AddPurchaseAsync(Family family, Purchase purchase);
    }
}
