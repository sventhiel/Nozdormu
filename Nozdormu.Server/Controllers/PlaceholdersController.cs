using Microsoft.AspNetCore.Mvc;

namespace Nozdormu.Server.Controllers
{
    public class PlaceholdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
