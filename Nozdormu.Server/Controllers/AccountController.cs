using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nozdormu.Server.Models;

namespace Nozdormu.Server.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAccountModel model)
        {
            return View(model);
        }
    }
}
