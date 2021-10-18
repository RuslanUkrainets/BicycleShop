using System;
using System.ComponentModel.DataAnnotations;

namespace BicycleShop.Models.ViewModels
{
    public class ChangeUserViewModel
    {
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int? Year { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
