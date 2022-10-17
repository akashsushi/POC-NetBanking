using BankingApplication.BusinessLayer.Contracts;
using BankingApplication.CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICustomerAsyncManager customerManager;

        public HomeController(ICustomerAsyncManager customerManager)
        {
            this.customerManager = customerManager;
        }

        //POST: /api/Home/FundTransfer
        [Route("/api/Home/FundTransfer")]
        [HttpPost]
        public async Task<ActionResult> FundTransfer(Transaction transaction)
        {
            var tId = 0;

            try
            {
                if (transaction.TransactionType.Equals("Transfer"))
                {
                    var transfer = new CommonLayer.Models.Transaction()
                    {
                        SourceAccountNo = transaction.SourceAccountNo,
                        DestinationAccountNo = transaction.DestinationAccountNo,
                        TransactionDate = DateTime.UtcNow,
                        TransactionAmount = transaction.TransactionAmount,
                        TransactionType = transaction.TransactionType,
                        TransactionDescription = transaction.TransactionDescription
                    };
                    tId = await this.customerManager.Transfer(transaction);
                }

                if (tId != 0)
                {
                    return Ok($"Amount:{transaction.TransactionAmount} is Transfered from {transaction.SourceAccountNo} " +
                        $"to {transaction.DestinationAccountNo}");
                }
                return BadRequest("Unable to transfer");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Performing Transaction");
            }
        }

        //GET: /api/Home/BalanceEnquiry/{accountNumber}
        [Route("/api/Home/BalanceEnquiry/{accountNumber}")]
        [HttpGet]
        public async Task<ActionResult> BalanceEnquiry(string accountNumber)
        {
            try
            {
                var account = await this.customerManager.GetBalance(accountNumber);
                if (account == null)
                {
                    return BadRequest("Account doesn't exist");
                }
                return Ok($"{accountNumber} Balance is {account.Balance}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Performing Transaction");
            }
        }

        //GET: /api/Home/CustomizedStatement
        [Route("/api/Home/CustomizedStatement")]
        [HttpGet]
        public async Task<ActionResult> CustomizedStatement(CustomizedStatement statementVM)
        {
            try
            {
                //if (statementVM.FromDate <= statementVM.ToDate)
                //{
                //    return BadRequest("Invalid date format");
                //}

                var transactions = await this.customerManager.CustomizedStatement(statementVM);
                if (transactions == null)
                {
                    return NotFound();
                }
                return Ok(transactions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Performing Transaction");
            }
        }

        //GET: /api/Home/MiniStatement/
        [HttpGet]
        [Route("/api/Home/MiniStatement/{accountNo}")]
        public async Task<ActionResult> MiniStatement(string accountNo)
        {
            try
            {
                var transactions = await this.customerManager.MiniStatement(accountNo);
                if (transactions == null)
                {
                    return NotFound();
                }
                return Ok(transactions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Performing Transaction");
            }
        }
    }
}
