using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nozdormu.Server.Configurations;
using Nozdormu.Server.Models;
using Nozdormu.Server.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace Nozdormu.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Authentication

        public IActionResult Login()
        {
            return View(new LoginUserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            if (ModelState.IsValid)
            {
                var userService = new UserService(_connectionString);

                if (!userService.Verify(model.Username, model.Password))
                    return View();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        #endregion Authentication

        [Authorize]
        public IActionResult Privacy()
        {
            var username = User.Identity.Name;

            return View("Privacy", username);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}