using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nozdormu.Server.Configurations;
using Nozdormu.Server.Models;
using Nozdormu.Server.Services;

namespace Nozdormu.Server.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ConnectionString _connectionString;

        public AccountController(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public IActionResult Index()
        {
            var accountService = new AccountService(_connectionString);

            var accounts = accountService.Find().Select(a => ReadAccountModel.Convert(a));

            return View(accounts);
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