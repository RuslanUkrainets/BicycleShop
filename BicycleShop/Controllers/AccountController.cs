using BicycleShop.Models;
using BicycleShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            
            if (result.Succeeded)
            {
                return !string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl) 
                    ? Redirect(model.ReturnUrl) : RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid email and/or password");
            return View(model);
        }

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            User user = new()
            {
                Email = model.Email,
                UserName = model.Email,
                Year = model.Year
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            var userRole = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == "User");
            if (userRole != null)
            {
                await _userManager.AddToRoleAsync(user, userRole.Name);
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangeUser() => View(_userManager.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("ChangeUser");

            var user = await _userManager.FindByIdAsync(userId);
            ChangeUserViewModel model = new()
            {
                Email = user.Email,
                UserName = user.UserName,
                Year = user.Year,
                UserId = userId
            };                       

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChangeUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if(!ModelState.IsValid) return View(model);

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.Year = model.Year.Value;

            if(!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded)
                return RedirectToAction("ChangeUser");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            
            return View(result);
        }

        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound();

            var changePassVM = new ChangePasswordViewModel()
            {
                UserId = user.Id,
                Email = user.Email
            };
            return View(changePassVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null) return NotFound();

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}
