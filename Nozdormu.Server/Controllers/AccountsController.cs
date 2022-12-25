using LiteDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nozdormu.Server.Configurations;
using Nozdormu.Server.Entities;
using Nozdormu.Server.Models;
using Nozdormu.Server.Services;

namespace Nozdormu.Server.Controllers
{
    public class AccountsController : Controller
    {
        private ConnectionString _connectionString;

        public AccountsController(ConnectionString connectionString)
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
            return View(new CreateAccountModel());
        }

        [HttpPost]
        public IActionResult Create(CreateAccountModel model)
        {
            if(ModelState.IsValid)
            {
                var accountService = new AccountService(_connectionString);
                accountService.Create(model.Host, model.Prefix, model.Name, model.Password);

                return RedirectToAction("Index", "Accounts");
            }

            return View(model);
        }

        public IActionResult Update(int id)
        {
            var accountService = new AccountService(_connectionString);
            var account = accountService.FindById(id);
            return View(UpdateAccountModel.Convert(account));
        }

        [HttpPost]
        public IActionResult Update(UpdateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                var accountService = new AccountService(_connectionString);
                var account = new Account()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Host = model.Host,
                    Password = model.Password,
                    Prefix = model.Prefix
                };

                accountService.Update(account);
                
                return RedirectToAction("Index", "Accounts");
            }
            
            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var accountService = new AccountService(_connectionString);
            accountService.DeleteById(id);

            return RedirectToAction("Index", "Accounts");
        }
    }
}