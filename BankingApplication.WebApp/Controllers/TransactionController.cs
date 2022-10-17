using BankingApplication.BusinessLayer.Contracts;
using BankingApplication.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Controllers
{
    public class TransactionController : Controller
    {
        #region Private 
        private readonly ICustomerManager customerManager;
        private readonly IAccountManager accountManager;
        private readonly ITransactionManager transactionManager;
        #endregion

        #region Public Methods
        public TransactionController(ICustomerManager customerManager, IAccountManager accountManager,ITransactionManager transactionManager)
        {
            this.customerManager = customerManager;
            this.accountManager = accountManager;
            this.transactionManager = transactionManager;
        }

        [HttpGet]
        public IActionResult FundTransfer()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult FundTransfer(FundTransferVM transferVM)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }

            var tId = 0;
            if (ModelState.IsValid)
            {
                if (transferVM.TransactionType.Equals("Transfer"))
                {
                    var transfer = new CommonLayer.Models.Transaction()
                    {
                        SourceAccountNo = transferVM.SourceAccountNo,
                        DestinationAccountNo = transferVM.DestinationAccountNo,
                        TransactionDate = DateTime.UtcNow,
                        TransactionAmount = transferVM.TransactionAmount,
                        TransactionType = transferVM.TransactionType,
                        TransactionDescription = transferVM.TransactionDescription
                    };
                    tId = this.transactionManager.Transfer(transfer);
                }
            }
            if (tId != 0)
            {
                ViewData["message"] = "Transaction with id:" + tId + " is successfull";
                return View("Confirm");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Withdrawal()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Withdrawal(WithdrawalVM transactionVM)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            var tId = 0;
            if (ModelState.IsValid)
            {
                if (transactionVM.TransactionType.Equals("Withdrawal"))
                {
                    var transfer = new CommonLayer.Models.Transaction()
                    {
                        SourceAccountNo = transactionVM.SourceAccountNo,
                        TransactionDate = DateTime.UtcNow,
                        TransactionAmount = transactionVM.TransactionAmount,
                        TransactionType = transactionVM.TransactionType,
                        TransactionDescription = transactionVM.TransactionDescription
                    };
                    tId = this.transactionManager.Withdrawal(transfer);
                }
            }
            if (tId != 0)
            {
                ViewData["message"] = "Money has been Withdrawn from Account:"+ transactionVM.SourceAccountNo + " successfully with " +
                                                        "transaction id:" + tId + " is successfull";
                return View("Confirm");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        
        [HttpPost]
        public IActionResult Deposit(DepositVM depositVM)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            var tId = 0;
            if (ModelState.IsValid)
            {
                if (depositVM.TransactionType.Equals("Deposit"))
                {
                    var transfer = new CommonLayer.Models.Transaction()
                    {
                        DestinationAccountNo = depositVM.DestinationAccountNo,
                        TransactionDate = DateTime.UtcNow,
                        TransactionAmount = depositVM.TransactionAmount,
                        TransactionType = depositVM.TransactionType,
                        TransactionDescription = depositVM.TransactionDescription
                    };
                    tId = this.transactionManager.Deposit(transfer);
                }
            }
            if (tId != 0)
            {
                ViewData["message"] = "Money has been deposited successfully to Account: "+depositVM.DestinationAccountNo+" with transaction ID:" + tId;
                return View("Confirm");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetTransactions()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetTransactions(GetAccountVM accountVM)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            TempData["accountId"] = accountVM.AccountId;
            return RedirectToAction("MiniStatement");
        }

        [HttpGet]
        public IActionResult MiniStatement()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            if(TempData["accountId"] == null)
            {

                return RedirectToAction("GetTransactions");
            }
            var accountNo = TempData["accountId"].ToString();
            var accounts = this.transactionManager.MiniStatement(accountNo);
            return View(accounts);
        }

        [HttpGet]
        public IActionResult CustomizedStatementFilter()
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CustomizedStatementFilter(CustomizedStatementVM statementVM)
        {
            if (SessionValid())
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                if(statementVM.FromDate >= statementVM.ToDate)
                {
                    ViewData["CustomizedFilterError"] = "From date and To date are invalid";
                    return View();
                }
                else
                {
                    var custom = new CommonLayer.Models.CustomizedStatement()
                    {
                        AccountNo = statementVM.AccountNo,
                        FromDate = statementVM.FromDate,
                        ToDate = statementVM.ToDate,
                        LowerLimit = statementVM.LowerLimit,
                        NumberOfTransaction = statementVM.NumberOfTransaction
                    };
                    var accounts = this.transactionManager.CustomizedStatement(custom);
                    if(accounts != null)
                    {
                        return View("MiniStatement", accounts);
                    }
                }
            }
            return View();
        }

        #endregion

        #region Private Methods

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
