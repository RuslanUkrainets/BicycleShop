using BicycleShop.Config;
using BicycleShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BicycleShop.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private const string ROLE = "Admin";
        private Person _person = new()
        {
            Login = "admin",
            Password = "admin",
            Role = "admin"
        };

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] Person person)
        {
            var identity = await GetIdentity(person.Login, person.Password);
            if (identity == null)
                return BadRequest(new { error = "Invalid login or password" });

            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return Json(response);
        }
        
        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            var user = await _userManager.FindByNameAsync(login);
            var result = await _userManager.CheckPasswordAsync(user, password);

            if(!result) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var isAdmin = roles.Contains(ROLE);

            if (!isAdmin) return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, ROLE)
            };

            ClaimsIdentity claimsIdentity = 
                new (claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                
            return claimsIdentity;
        }
    }

}
