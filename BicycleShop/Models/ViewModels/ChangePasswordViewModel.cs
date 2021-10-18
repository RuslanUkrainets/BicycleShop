using System.ComponentModel.DataAnnotations;

namespace BicycleShop.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
