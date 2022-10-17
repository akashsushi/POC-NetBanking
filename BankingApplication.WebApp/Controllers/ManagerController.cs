using BankingApplication.BusinessLayer.Contracts;
using BankingApplication.CommonLayer.Models;
using BankingApplication.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Controllers
{
    public class ManagerController : Controller
    {
        #region Private
        private readonly ICustomerManager customerManager;
        private readonly IAccountManager accountManager;
        #endregion

        #region Public Methods
        public ManagerController(ICustomerManager customerManager,IAccountManager accountManager)
        {
            this.customerManager = customerManager;
            this.accountManager = accountManager;
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(CreateCustomerVM customerVM)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {

                if (customerVM.Dob < DateTime.Now)
                {
                    var managerId = HttpContext.Session.GetString("userId");
                    var customerEmail = customerManager.GetCustomer(customerVM.EmailId, managerId, true);
                    if (customerEmail != null)
                    {
                        ViewData["AddError"] = "Email-Id already Exist,Customer Creation Failed!";
                        return View();
                    }

                    var newCustomer = new Customer()
                    {
                        FirstName = customerVM.FirstName,
                        LastName = customerVM.LastName,
                        Dob = customerVM.Dob,
                        EmailId = customerVM.EmailId,
                        MobileNumber = customerVM.MobileNumber,
                        Gender = customerVM.Gender,
                        City = customerVM.City,
                        State = customerVM.State,
                        Pincode = customerVM.Pincode,
                        ManagerId = managerId
                    };
                    string customerId = customerManager.AddCustomer(newCustomer);
                    if (customerId == null)
                    {
                        ViewData["AddError"] = "Failed to add Customer";
                    }
                    else
                    {
                        ViewData["message"] = "Successfully added customer with ID: " + customerId;
                        return View("Confirm");
                    }
                }
                else
                {
                    ViewData["AddError"] = "Invalid DOB, Customer Creation Failed!";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetCustomer(GetCustomerVM getCustomer)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            var managerId = HttpContext.Session.GetString("userId");
            var customer = this.customerManager.GetCustomer(getCustomer.CustomerId,managerId);
            if(customer == null)
            {
                ViewData["GetCustomer"] = "Customer with given ID not found or is unable to access";
                return View();
            }
            TempData["customerId"] = getCustomer.CustomerId;
            return RedirectToAction("EditCustomer");
        }

        [HttpGet]
        public IActionResult EditCustomer(Customer customer)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
            
        [HttpPost]
        public IActionResult EditCustomer(UpdateCustomerVM customerVM)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string customerId = Convert.ToString(TempData["customerId"]);
                var updateCustomer = new BankingApplication.CommonLayer.Models.Customer()
                {
                    CustomerId = customerId,
                    FirstName = customerVM.FirstName,
                    LastName = customerVM.LastName,
                    Dob = customerVM.Dob,
                    EmailId = customerVM.EmailId,
                    MobileNumber = customerVM.MobileNumber,
                    Gender = customerVM.Gender,
                    City = customerVM.City,
                    State = customerVM.State,
                    Pincode = customerVM.Pincode
                };
                if (customerManager.UpdateCustomer(updateCustomer))
                {
                    ViewData["message"] = "Successfully updated customer with ID: " + customerId;
                    return View("Confirm");
                }
            }
            ViewData["UpdateError"] = "Unable to update Customer";
            return View();
        }

        [HttpGet]
        public IActionResult DeleteCustomer()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult DeleteCustomer(GetCustomerVM customerVM)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            if (this.customerManager.DeleteCustomer(customerVM.CustomerId))
            {
                ViewData["message"] = "Successfully deleted customer with ID: " + customerVM.CustomerId;
                return View("Confirm");
            }
            ViewData["DeleteCustomerError"] = "Failed to delete the account";
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(AccountCreationVM accountVM)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            string acNo = string.Empty;

            if (ModelState.IsValid)
            {
                var newAccount = new Account()
                {
                    AccountType = accountVM.AccountType,
                    Balance = accountVM.Balance,
                    CustomerId = accountVM.CustomerId,
                    Doc = DateTime.UtcNow,
                    Ifsc = accountVM.IFSC,
                    Tin = accountVM.Tin
                };

                if (accountVM.AccountType.Equals("saving"))
                {
                    var oldAccount = accountManager.GetAccount(newAccount);

                    if (oldAccount == null)
                    {
                        acNo = AddNewAccount(newAccount);
                    }
                }

                else if (accountVM.AccountType.Equals("current"))
                {
                    var oldAccount = accountManager.GetCustomerAccounts(accountVM.CustomerId);
                    foreach(var acc in oldAccount)
                    {
                        if (acc.Tin.Equals(accountVM.Tin))
                        {
                            ViewData["CreateAccount"] = "TIN already registered";
                            return View();
                        }
                    }
                    acNo = AddNewAccount(newAccount);
                }

                else if (accountVM.AccountType.Equals("corporate"))
                {
                    var oldAccount = accountManager.GetAccount(newAccount);
                    if(oldAccount == null)
                    {
                        acNo = AddNewAccount(newAccount);
                    }
                }
            }

            if (String.IsNullOrEmpty(acNo))
            {
                ViewData["CreateAccount"] = "Failed to create the account";
                return View();
            }

            ViewData["message"] = "Account created with" + acNo;
            return View("Confirm");
        }

        [HttpGet]
        public IActionResult GetAccount()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetAccount(GetAccountVM getAccount)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }

            var account = this.accountManager.GetAccount(getAccount.AccountId);

            if (account == null)
            {
                //ViewData["message"] = "Failed to find the account";
                ModelState.AddModelError("Password", "Invalid login attempt.");
                return View();
            }

            TempData["accountId"] = account.AccountNumber;
            TempData["accountType"] = account.AccountType;

            string action = string.Empty;

            if (TempData["request"].ToString().Equals("update"))
            {
                 action = "UpdateAccount";
            }

            if (TempData["request"].ToString().Equals("delete"))
            {
                 action = "DeleteAccount";
            }

            if (TempData["request"].ToString().Equals("balance"))
            {
                action = "BalanceEnquiry";
            }

            return RedirectToAction(action);
        }

        [HttpGet]
        public IActionResult UpdateAccount()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            
            if (TempData["accountId"] == null)
            {
                
                TempData["request"] = "update";
                return RedirectToAction("GetAccount");
            }
            TempData["account"] = TempData["accountId"].ToString();
            return View();
        }

        [HttpPost]
        public IActionResult UpdateAccount(UpdateAccountVM updateAccount)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            var accountNo = Convert.ToString(TempData["account"]);
            if (ModelState.IsValid)
            {
                bool isUpdated = false;
                var accountType = Convert.ToString(TempData["accountType"]);

                if (updateAccount.AccountType.Equals(accountType))
                {
                    isUpdated = UpdateAccount(updateAccount,accountNo);
                }

                else
                {
                    isUpdated = UpdateAccount(updateAccount, accountNo, true);
                }
                if (!isUpdated)
                {
                    ViewData["UpdateError"] = "Failed to update the account";
                    return View();
                }
            }
            ViewData["message"] = accountNo +"updated successfully" ;
            return View("Confirm");
        }

        [HttpGet]
        public IActionResult DeleteAccount()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            if (TempData["accountId"] == null)
            {
                TempData["request"] = "delete"; 
                return RedirectToAction("GetAccount");
            }
            TempData["account"] = TempData["accountId"].ToString();
            ViewBag.AccountNo = TempData["accountId"].ToString();
            return View();
        }

        [HttpPost]
        public IActionResult DeleteAccount(DeleteAccountVM accountVM)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                var accountNo = Convert.ToString(TempData["account"]);
                var accountType = this.accountManager.GetAccount(accountNo);

                if (accountVM.IsDelete.Equals("Yes"))
                {
                    if (accountManager.DeleteAccount(accountNo))
                    {
                        ViewData["message"] = "Account with AC: "+accountNo + "successfully deleted";
                        return View("Confirm");
                    }
                }  
            }
            ViewData["DeleteACError"] = "Failed to delete the account";
            return View();
        }

        [HttpGet]
        public IActionResult BalanceEnquiry()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }

            if (TempData["accountId"] == null)
            {
                TempData["request"] = "balance";
                return RedirectToAction("GetAccount");
            }

            var accountNo = TempData["accountId"].ToString();
            var account = this.accountManager.GetAccount(accountNo);
            ViewBag.AccountNo = account.AccountNumber;
            ViewBag.Balance = account.Balance;
            return View();
        }

        #endregion

        #region Private methods

        private string AddNewAccount(Account account)
        {
            return accountManager.AddAccount(account);
        }

        private bool UpdateAccount(UpdateAccountVM updateAccount, string accountNo, bool isType = false)
        {
            bool isUpdated = false;
            var account = new CommonLayer.Models.Account()
            {
                AccountNumber = accountNo,
                AccountType = updateAccount.AccountType,
                CustomerId = updateAccount.CustomerId,
                Balance = updateAccount.Balance,
                Doc = updateAccount.DOC,
                Ifsc = updateAccount.IFSC,
                Tin = updateAccount.Tin
        };
            
            if (isType)
            {
                isUpdated = accountManager.UpdateAccount(account,true);
            }
            else
            {
                isUpdated = accountManager.UpdateAccount(account);
            }
            return isUpdated;
        }

        private bool SessionValid()
        {
            bool isValid = false;

            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                isValid = true;
            }

            return isValid;
        }

        #endregion
    }
}
