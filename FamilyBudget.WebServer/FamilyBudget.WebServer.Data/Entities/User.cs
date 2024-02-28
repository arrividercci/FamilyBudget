using Microsoft.AspNetCore.Identity;

namespace FamilyBudget.WebServer.Data.Entities
{
    public class User : IdentityUser
    {
        public double Balance { get; set; }
        public ICollection<Purchase> Purchases { get; set; } = default!;
        public ICollection<Family> Families { get; set; } = default!;
    }
}
