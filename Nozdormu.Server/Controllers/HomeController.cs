using LiteDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nozdormu.Server.Configurations;
using Nozdormu.Server.Models;
using Nozdormu.Server.Services;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Nozdormu.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ConnectionString _connectionString;
        private JwtConfiguration _jwtConfiguration;
        private List<Admin> _admins;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ConnectionString connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
            _jwtConfiguration = configuration.GetSection("JWT").Get<JwtConfiguration>();
            _admins = configuration.GetSection("Admins").Get<List<Admin>>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Authentication

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginUserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model, string returnUrl = "")
        {
            //
            // Check Admin Configuration first
            var admin = _admins.Find(a => a.Name.Equals(model.Name));

            if (admin != null && admin.Password.Equals(model.Password))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.Name),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                if (Url.IsLocalUrl(returnUrl))
                {
                    ViewBag.ReturnUrl = returnUrl;
                    return Redirect(returnUrl);

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                var userService = new UserService(_connectionString);

                if (!userService.Verify(model.Name, model.Password))
                    return View();

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                if (Url.IsLocalUrl(returnUrl))
                {
                    ViewBag.ReturnUrl = returnUrl;
                    return Redirect(returnUrl);

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }


        }

        [HttpPost]
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