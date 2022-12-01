using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Nozdormu.Server.Models;
using Nozdormu.Server.Services;
using Nozdormu.Server.Utilities;
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
