﻿using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.WebServer.Mvc.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
