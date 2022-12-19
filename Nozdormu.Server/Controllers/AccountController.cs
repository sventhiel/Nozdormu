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
        private readonly ILogger<HomeController> _logger;
        private ConnectionString _connectionString;

        public AccountController(ILogger<HomeController> logger, ConnectionString connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        public IActionResult Index()
        {
            var accountService = new AccountService(_connectionString);

            var accounts = accountService.Find();

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