using BankingApplication.EFLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.EFLayer.Implementations
{
    public class AccountMinRepository
    {
        private readonly db_bankingContext dbContext = null;
        public AccountMinRepository(db_bankingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public double MinSavings()
        {
            double minBalance = 0;
            var minSaving = this.dbContext.SavingsAccounts.FirstOrDefault(s => s.Ind == 1);
            if(minSaving != null)
            {
                minBalance = minSaving.MinimumBalance;
            }
            return minBalance;
        }

        public double MinCurrent()
        {
            double minBalance = 0;
            var minCurrent = this.dbContext.CurrentAccounts.FirstOrDefault(s => s.Ind == 2);
            if (minCurrent != null)
            {
                minBalance = minCurrent.MinimumBalance;
            }
            return minBalance;
        }

        public double MinCorporate()
        {
            double minBalance = 0;
            var minCorporate = this.dbContext.CorporatetAccounts.FirstOrDefault(s => s.Ind == 3);
            if (minCorporate != null)
            {
                minBalance = minCorporate.MinimumBalance;
            }
            return minBalance;
        }

        public bool SavingsDailyLimit(IEnumerable<CommonLayer.Models.Transaction> transactions,double transactionAmount)
        {
            bool isValid = false;
            double totalTransfer = transactionAmount;
            var minSaving = this.dbContext.SavingsAccounts.FirstOrDefault(s => s.Ind == 1);
            foreach (var transfer in transactions)
            {
                totalTransfer += transfer.TransactionAmount;
            }
            if(totalTransfer <= minSaving.WithdrawlLimit)
            {
                isValid = true;
            }
            return isValid;
        }

        public bool CurrentDailyLimit(IEnumerable<CommonLayer.Models.Transaction> transactions, double transactionAmount)
        {
            bool isValid = false;
            double totalTransfer = transactionAmount;
            var minCurrent = this.dbContext.CurrentAccounts.FirstOrDefault(s => s.Ind == 2);
            foreach (var transfer in transactions)
            {
                totalTransfer += transfer.TransactionAmount;
            }
            if (totalTransfer <= minCurrent.WithdrawlLimit)
            {
                isValid = true;
            }
            return isValid;
        }

        public bool CorporateDailyLimit(IEnumerable<CommonLayer.Models.Transaction> transactions, double transactionAmount)
        {
            bool isValid = false;
            double totalTransfer = transactionAmount;
            var minCorporate = this.dbContext.CorporatetAccounts.FirstOrDefault(s => s.Ind == 3);
            foreach (var transfer in transactions)
            {
                totalTransfer += transfer.TransactionAmount;
            }
            if (totalTransfer <= minCorporate.WithdrawlLimit)
            {
                isValid = true;
            }
            return isValid;
        }

    }
}
