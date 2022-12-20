using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nozdormu.Server.Configurations;
using Nozdormu.Server.Models;
using Nozdormu.Server.Services;
using Nozdormu.Server.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        //[Authorize]
        //public IActionResult Profile()
        //{
        //    if (User?.Identity == null)
        //        return "";

        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.IssuerSigningKey));

        //    var token = new JwtSecurityToken(
        //        issuer: _jwtConfiguration.ValidIssuer,
        //        audience: _jwtConfiguration.ValidAudience,
        //        expires: DateTime.Now.AddHours(_jwtConfiguration.ValidLifetime),
        //        claims: new[] { new Claim(ClaimTypes.Name, User.Identity.Name) },
        //signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512)
        //        );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            //
            // Create an instance of the user service to get all users from the database.
            var userService = new UserService(_connectionString);

            //
            // Instead of returning all users from the database, each of them is
            // converted into a specific model, containing the properties
            // that should show up in the table of the view.
            var users = userService.Find().Select(u => ReadUserModel.Convert(u));

            return View(users);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            //
            // Simply return the view with an empty model.
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                //
                // Create an instance of the user service to create a new user to the database.
                var userService = new UserService(_connectionString);

                //
                // Call the user service with necessary properties to create a new user.
                userService.Create(model.Name, model.Password, model.Pattern, model.AccountId);

                //
                // After creation of the new user, redirect to the table of all users.
                return RedirectToAction("Index", "Users");
            }

            return View(model);
        }
    }
}