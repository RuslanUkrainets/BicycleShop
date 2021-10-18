using Microsoft.AspNetCore.Identity;

namespace BicycleShop.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
