using FamilyBudget.WebServer.Data.Entities;
using FamilyBudget.WebServer.Data.Models;

namespace FamilyBudget.WebServer.Data.Extensions
{
    public static class FamilyExtensions
    {
        public static FamilyDto? ToFullModel(this Family? family)
        {
            if (family == null)
            {
                return null;
            }
            else
            {
                return new FamilyDto()
                {
                    Id = family.Id,
                    Name = family.Name,
                    Balance = family.Purchases.Sum(purchase => purchase.Price),
                    Purchases = family.Purchases!.Select(purchase => purchase.ToFamilyModel()).ToList()!,
                    Users = family.Users!.Select(user => user.ToFamilyModel()).ToList()!,
                };
            }
        }

        public static FamilyForListDto? ToModel(this Family? family)
        {
            if (family == null)
            {
                return null;
            }
            else
            {
                return new FamilyForListDto()
                {
                    Id = family.Id,
                    Name = family.Name
                };
            }
        }
    }
}
