using BankingApplication.CommonLayer.Models;
using BankingApplication.DataLayer.Contracts;
using BankingApplication.EFLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BankingApplication.EFLayer.Implementations
{
    public class TransactionRepositoryEFImpl:ITransactionRepository
    {
        #region Private
        private readonly db_bankingContext dbContext = null;
        private readonly AccountRepositoryEFImpl accountRepository;
        private readonly AccountMinRepository minRepository;
        #endregion

        #region Public methods
        public TransactionRepositoryEFImpl(db_bankingContext dbContext)
        {
            this.dbContext = dbContext;
            this.accountRepository = new AccountRepositoryEFImpl(dbContext);
            this.minRepository = new AccountMinRepository(dbContext);
        }

        public int Transfer(CommonLayer.Models.Transaction transaction)
        {
            int tId = 0; 
            var sourceAccountDb = this.dbContext.Accounts.FirstOrDefault(a => a.AccountNumber.Equals(transaction.SourceAccountNo)
                                                                       && a.IsDeleted == false);
            var destinationAccountDb = this.dbContext.Accounts.FirstOrDefault(a => a.AccountNumber.Equals(transaction.DestinationAccountNo)
                                                                       && a.IsDeleted == false);
            var transactionsDb = this.dbContext.Transactions;
            var isDailyLimit = ValidateTransactionLimit(transactionsDb, sourceAccountDb, transaction);

            if (sourceAccountDb != null && destinationAccountDb != null)
            {
                if(sourceAccountDb.Balance >= transaction.TransactionAmount && (!transaction.SourceAccountNo.Equals(transaction.DestinationAccountNo)) 
                        && isDailyLimit )
                {
                    sourceAccountDb.Balance -= transaction.TransactionAmount;
                    this.dbContext.SaveChanges();
                    destinationAccountDb.Balance += transaction.TransactionAmount;
                    this.dbContext.SaveChanges();
                    var newTransaction = new Models.Transaction()
                    {
                        SourceAccountNo = transaction.SourceAccountNo,
                        DestinationAccountNo = transaction.DestinationAccountNo,
                        TransactionAmount = transaction.TransactionAmount,
                        TransactionDate = transaction.TransactionDate,
                        TransactionType = transaction.TransactionType,
                        TransactionDescription = transaction.TransactionDescription
                    };
                    this.dbContext.Transactions.Add(newTransaction);
                    this.dbContext.SaveChanges();
                    tId = newTransaction.TransactionId;
                }
            }
            return tId;
        }

        public int Withdrawal(CommonLayer.Models.Transaction transaction)
        {
            int tId = 0;
            var sourceAccountDb = this.dbContext.Accounts.FirstOrDefault(a => a.AccountNumber.Equals(transaction.SourceAccountNo)
                                                                       && a.IsDeleted == false);

            if (sourceAccountDb != null)
            {
                var transactionsDb = this.dbContext.Transactions;
                var isDailyLimit = ValidateTransactionLimit(transactionsDb, sourceAccountDb, transaction);

                if ((sourceAccountDb.Balance >= transaction.TransactionAmount) && isDailyLimit)
                {
                    sourceAccountDb.Balance -= transaction.TransactionAmount;
                    var newTransaction = new Models.Transaction()
                    {
                        SourceAccountNo = transaction.SourceAccountNo,
                        TransactionAmount = transaction.TransactionAmount,
                        TransactionDate = transaction.TransactionDate,
                        TransactionType = transaction.TransactionType,
                        TransactionDescription = transaction.TransactionDescription
                    };
                    this.dbContext.Transactions.Add(newTransaction);
                    this.dbContext.SaveChanges();
                    tId = newTransaction.TransactionId;
                } 
            }
            return tId;
        }

        public int Deposit(CommonLayer.Models.Transaction transaction)
        {
            int tId = 0;
            var destinationAccountDb = this.dbContext.Accounts.FirstOrDefault(a => a.AccountNumber.Equals(transaction.DestinationAccountNo)
                                                                       && a.IsDeleted == false);

            if (destinationAccountDb != null)
            {
                destinationAccountDb.Balance += transaction.TransactionAmount;
                this.dbContext.SaveChanges();
                var newTransaction = new Models.Transaction()
                {
                    DestinationAccountNo = transaction.DestinationAccountNo,
                    TransactionAmount = transaction.TransactionAmount,
                    TransactionDate = transaction.TransactionDate,
                    TransactionType = transaction.TransactionType,
                    TransactionDescription = transaction.TransactionDescription
                };
                this.dbContext.Transactions.Add(newTransaction);
                this.dbContext.SaveChanges();
                tId = newTransaction.TransactionId; 
            }
            return tId;
        }

        public IEnumerable<CommonLayer.Models.Transaction> MiniStatement(string accountNo)
        {
            var transactionsDb = this.dbContext.Transactions;
            List<CommonLayer.Models.Transaction> transactions = null;
            if (transactionsDb != null)
            {
                transactions = (from transactionDb in transactionsDb
                                where transactionDb.SourceAccountNo.Equals(accountNo) 
                                || transactionDb.DestinationAccountNo.Equals(accountNo)
                                orderby transactionDb.TransactionId descending
                                select new CommonLayer.Models.Transaction() {
                                    TransactionId = transactionDb.TransactionId,
                                    SourceAccountNo = transactionDb.SourceAccountNo,
                                    DestinationAccountNo = transactionDb.DestinationAccountNo,
                                    TransactionAmount = transactionDb.TransactionAmount,
                                    TransactionDate = transactionDb.TransactionDate,
                                    TransactionType = transactionDb.TransactionType,
                                    TransactionDescription = transactionDb.TransactionDescription
                                }).Take(5).ToList();
            }

            return transactions;
        }

        public IEnumerable<CommonLayer.Models.Transaction> CustomizedStatement(CustomizedStatement customizedStatement)
        {
            var transactionsDb = this.dbContext.Transactions;
            List<CommonLayer.Models.Transaction> transactions = null;
            int count = customizedStatement.NumberOfTransaction;
            if (transactionsDb != null)
            {
                transactions = (transactions = (from transactionDb in transactionsDb
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
                                                }).Take(count).ToList());
            }
            return transactions;
        }
        #endregion

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
