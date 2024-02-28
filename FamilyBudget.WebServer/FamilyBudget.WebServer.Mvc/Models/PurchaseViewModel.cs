using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.WebServer.Mvc.Models
{
    public class PurchaseViewModel
    {
        [Display(Name = "Name")]
        public string? Name {  get; set; }
        [Display(Name = "Price")]
        public double Price { get; set; }
        [Display(Name = "Is Replenishment")]
        public bool IsReplenishment { get; set; }
    }
}
