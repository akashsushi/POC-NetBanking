using BankingApplication.CommonLayer.Models;
using BankingApplication.DataLayer.Contracts;
using BankingApplication.EFLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.EFLayer.Implementations
{
    public class CustomerAsyncImpl : ICustomerAsyncRepository
    {
        private readonly db_bankingContext db_Banking;
        private readonly AccountMinRepository minRepository;

        public CustomerAsyncImpl(db_bankingContext db_Banking)
        {
            this.db_Banking = db_Banking;
            this.minRepository = new AccountMinRepository(db_Banking);
        }

        public async Task<CommonLayer.Models.Account> GetBalance(string id)
        {
            CommonLayer.Models.Account account = null;
            var accountDb = await this.db_Banking.Accounts.FirstOrDefaultAsync(a => a.AccountNumber.Equals(id)
                                                                       && a.IsDeleted == false);
            if (accountDb != null)
            {
                account = new CommonLayer.Models.Account()
                {
                    AccountNumber = accountDb.AccountNumber,
                    AccountType = accountDb.AccountType,
                    Tin = accountDb.Tin,
                    CustomerId = accountDb.CustomerId,
                    Balance = accountDb.Balance
                };
            }
            return account;
        }

        public async Task<int> Transfer(CommonLayer.Models.Transaction transaction)
        {
            int tId = 0;
            var sourceAccountDb = await this.db_Banking.Accounts.FirstOrDefaultAsync(a => a.AccountNumber.Equals(transaction.SourceAccountNo)
                                                                       && a.IsDeleted == false);
            var destinationAccountDb =await this.db_Banking.Accounts.FirstOrDefaultAsync(a => a.AccountNumber.Equals(transaction.DestinationAccountNo)
                                                                       && a.IsDeleted == false);
            var transactionsDb = this.db_Banking.Transactions;
            var isDailyLimit = ValidateTransactionLimit(transactionsDb, sourceAccountDb, transaction);

            if (sourceAccountDb != null && destinationAccountDb != null)
            {
                if (sourceAccountDb.Balance >= transaction.TransactionAmount && (!transaction.SourceAccountNo.Equals(transaction.DestinationAccountNo))
                        && isDailyLimit)
                {
                    sourceAccountDb.Balance -= transaction.TransactionAmount;
                    this.db_Banking.SaveChanges();
                    destinationAccountDb.Balance += transaction.TransactionAmount;
                    this.db_Banking.SaveChanges();
                    var newTransaction = new Models.Transaction()
                    {
                        SourceAccountNo = transaction.SourceAccountNo,
                        DestinationAccountNo = transaction.DestinationAccountNo,
                        TransactionAmount = transaction.TransactionAmount,
                        TransactionDate = transaction.TransactionDate,
                        TransactionType = transaction.TransactionType,
                        TransactionDescription = transaction.TransactionDescription
                    };
                    await this.db_Banking.Transactions.AddAsync(newTransaction);
                    this.db_Banking.SaveChanges();
                    tId = newTransaction.TransactionId;
                }
            }
            return tId;
        }

        public async Task<IEnumerable<CommonLayer.Models.Transaction>> MiniStatement(string accountNo)
        {
            var transactionsDb = this.db_Banking.Transactions;
            List<CommonLayer.Models.Transaction> transactions = null;
            if (transactionsDb != null)
            {
                transactions = await (from transactionDb in transactionsDb
                                where transactionDb.SourceAccountNo.Equals(accountNo)
                                || transactionDb.DestinationAccountNo.Equals(accountNo)
                                orderby transactionDb.TransactionId descending
                                select new CommonLayer.Models.Transaction()
                                {
                                    TransactionId = transactionDb.TransactionId,
                                    SourceAccountNo = transactionDb.SourceAccountNo,
                                    DestinationAccountNo = transactionDb.DestinationAccountNo,
                                    TransactionAmount = transactionDb.TransactionAmount,
                                    TransactionDate = transactionDb.TransactionDate,
                                    TransactionType = transactionDb.TransactionType,
                                    TransactionDescription = transactionDb.TransactionDescription
                                }).Take(5).ToListAsync();
            }

            return transactions;
        }

        public async Task<IEnumerable<CommonLayer.Models.Transaction>> CustomizedStatement(CustomizedStatement customizedStatement)
        {
            var transactionsDb = this.db_Banking.Transactions;
            List<CommonLayer.Models.Transaction> transactions = null;
            int count = customizedStatement.NumberOfTransaction;
            if (transactionsDb != null)
            {
                transactions = (transactions = await (from transactionDb in transactionsDb
                                                where (transactionDb.SourceAccountNo == customizedStatement.AccountNo
                                                || transactionDb.DestinationAccountNo == customizedStatement.AccountNo) &&
                                                (transactionDb.TransactionAmount >= customizedStatement.LowerLimit) ||
                                                (transactionDb.TransactionDate >= customizedStatement.FromDate ||
                                                transactionDb.TransactionDate <= customizedStatement.ToDate)
                                                orderby transactionDb.TransactionId descending
                                                select new CommonLayer.Models.Transaction()
                                                {
                                                    TransactionId = transactionDb.TransactionId,
                                                    SourceAccountNo = transactionDb.SourceAccountNo,
                                                    TransactionAmount = transactionDb.TransactionAmount,
                                                    TransactionType = transactionDb.TransactionType,
                                                    TransactionDate = transactionDb.TransactionDate,
                                                    DestinationAccountNo = transactionDb.DestinationAccountNo,
                                                    TransactionDescription = transactionDb.TransactionDescription
                                                }).Take(count).ToListAsync());
            }
            return transactions;
        }

        #region Private Methods
        private bool ValidateTransactionLimit(DbSet<Models.Transaction> transactionsDb, Models.Account sourceAccountDb, CommonLayer.Models.Transaction transaction)
        {
            bool isDailyLimit = false;
            var transactions = (from transactionDb in transactionsDb
                                where transactionDb.SourceAccountNo.Contains(transaction.SourceAccountNo)
                                && ((transactionDb.TransactionDate).Date.Equals((transaction.TransactionDate).Date))
                                select new BankingApplication.CommonLayer.Models.Transaction
                                {
                                    TransactionAmount = transactionDb.TransactionAmount
                                }).ToList();

            if (sourceAccountDb.AccountType.Equals("saving"))
            {
                isDailyLimit = this.minRepository.SavingsDailyLimit(transactions, transaction.TransactionAmount);
            }

            else if (sourceAccountDb.AccountType.Equals("current"))
            {
                isDailyLimit = this.minRepository.CurrentDailyLimit(transactions, transaction.TransactionAmount);
            }

            else
            {
                isDailyLimit = this.minRepository.CorporateDailyLimit(transactions, transaction.TransactionAmount);
            }
            return isDailyLimit;
        }

        #endregion
    }
}
