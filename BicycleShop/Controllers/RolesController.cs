using BicycleShop.Models;
using BicycleShop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleShop.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RolesController : Controller
    {
        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<User> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null) return NotFound();

            ChangeRoleViewModel model = new()
            {
                UserEmail = user.Email,
                UserId = user.Id
            };
            model.UserRoles = await _userManager.GetRolesAsync(user);
            model.Roles = await _roleManager.Roles.ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            return RedirectToAction("Index");
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name)) return View(name);

            var result = await _roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(name);
        }

        public IActionResult Delete()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);
            if(result.Succeeded)
                return RedirectToAction("Delete");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction("Delete");
        }
    }
}
