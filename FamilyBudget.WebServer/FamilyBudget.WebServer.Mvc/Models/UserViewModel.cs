using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.WebServer.Mvc.Models
{
    public class UserViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
