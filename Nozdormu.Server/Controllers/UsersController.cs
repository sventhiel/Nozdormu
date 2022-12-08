using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Nozdormu.Server.Configurations;
using Nozdormu.Server.Entities;
using Nozdormu.Server.Models;
using Nozdormu.Server.Services;
using Nozdormu.Server.Utilities;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Nozdormu.Server.Controllers
{
    public class UsersController : Controller
    {
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;

        public UsersController(IConfiguration configuration, ConnectionString connectionString)
        {
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
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
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                var token = GetToken();

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize]
        public string GetToken()
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.IssuerSigningKey));

            var token = new JwtSecurityToken(
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                expires: DateTime.Now.AddHours(_jwtConfiguration.ValidLifetime),
                claims: new[] { new Claim(ClaimTypes.Name, User.Identity.Name) },
        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
