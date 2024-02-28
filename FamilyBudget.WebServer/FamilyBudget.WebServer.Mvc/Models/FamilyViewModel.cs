using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.WebServer.Mvc.Models
{
    public class FamilyViewModel
    {
        [Display(Name = "Family name")]
        public string? Name { get; set; }
    }
}
