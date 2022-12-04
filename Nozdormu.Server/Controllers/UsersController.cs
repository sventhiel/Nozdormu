using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Nozdormu.Server.Entities;
using Nozdormu.Server.Models;
using Nozdormu.Server.Services;
using Nozdormu.Server.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Nozdormu.Server.Controllers
{
    public class UsersController : Controller
    {
        private ConnectionString _connectionString;

        public UsersController(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public IActionResult Login()
        {
            return View(new LoginUserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            if(model.Username == "admin")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Email, "asashd@sad.de"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                var token = new JwtSecurityTokenHandler().WriteToken(GetToken(claims));

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7033",
                audience: "https://localhost:7033",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPost]
        public IActionResult Logout(string x)
        {
            return View();
        }

        public IActionResult Index(string value = "hallo")
        {
            return View("Index", value);
        }

        public IActionResult Create()
        {
            var userService = new UserService(_connectionString);

            userService.Create("admin", "admin", "leer", 1);
            //
            // Instead of returning all users from the database, each of them is
            // converted into a specific model, containing the properties
            // that should show up in the table of the view.

            return View();
        }

        public IActionResult Verify()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Verify(LoginUserModel model)
        {
            var userService = new UserService(_connectionString);

            var user = userService.FindById(1);

            user.Token = CryptographyUtils.GetRandomHexadecimalString(32);

            userService.Update(user);

            var x = userService.Verify(model.Username, model.Password);

            return View("Index", x.ToString());
        }
    }
}
