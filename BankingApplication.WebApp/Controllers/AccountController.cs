using BankingApplication.BusinessLayer.Contracts;
using BankingApplication.WebApp.Models;
using BankingApplication.EFLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmployeeManager employeeManager;
        private readonly db_bankingContext context;
        private readonly ICustomerManager customerManager;

        public AccountController(IEmployeeManager employeeManager, db_bankingContext context,ICustomerManager customerManager)
        {
            this.employeeManager = employeeManager;
            this.context = context;
            this.customerManager = customerManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        { 
            if (ModelState.IsValid)
            {
                //var managerDetails = context.Managers.FirstOrDefault(x => x.EmailId == loginVM.LoginId && x.ManagerPassword == loginVM.Password);
                var managerDetails = this.employeeManager.GetManager(loginVM.LoginId);
                if (managerDetails == null || (!(managerDetails.ManagerPassword).Equals(loginVM.Password)))
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View();
                }
                HttpContext.Session.SetString("userId", managerDetails.ManagerId);
                HttpContext.Session.SetString("userName", managerDetails.FirstName);
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

    }
}
