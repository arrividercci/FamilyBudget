using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.WebServer.Mvc.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not same")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }
    }
}

