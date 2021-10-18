using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BicycleShop.Models.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
        public IList<string> UserRoles {  get; set; } = new List<string>();
    }
}
