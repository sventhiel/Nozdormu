using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nozdormu.Server.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Nozdormu.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
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