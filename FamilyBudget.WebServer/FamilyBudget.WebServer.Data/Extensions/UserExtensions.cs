using FamilyBudget.WebServer.Data.Entities;
using FamilyBudget.WebServer.Data.Models;

namespace FamilyBudget.WebServer.Data.Extensions
{
    public static class UserExtensions
    {
        public static UserForFamilyDto? ToFamilyModel(this User? user)
        {
            if (user == null)
            {
                return null;
            }
            else
            {
                return new UserForFamilyDto()
                {
                    Id = user.Id,
                    Email = user.Email
                };
            }
        }

        public static UserDto? ToModel(this User? user)
        {
            if (user == null)
            {
                return null;
            }
            else
            {
                return new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Balance = user.Purchases.Sum(purchase => purchase.Price),
                    Purchases = user.Purchases.Select(purchase => purchase.ToModel()).ToList()
                };
            }
        }

    }
}
