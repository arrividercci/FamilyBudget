using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyBudget.WebServer.Data.Entities
{
    public class Family
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public string? Name { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Purchase>? Purchases { get; set; }
    }
}
