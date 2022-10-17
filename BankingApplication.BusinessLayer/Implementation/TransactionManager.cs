using BankingApplication.BusinessLayer.Contracts;
using BankingApplication.CommonLayer.Models;
using BankingApplication.DataLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.BusinessLayer.Implementation
{
    public class TransactionManager : ITransactionManager
    {
        private readonly ITransactionRepository transactionRepository;

        public TransactionManager(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public int Deposit(Transaction transaction)
        => this.transactionRepository.Deposit(transaction);

        public int Transfer(Transaction transaction)
        => this.transactionRepository.Transfer(transaction);

        public int Withdrawal(Transaction transaction)
        => this.transactionRepository.Withdrawal(transaction);

        public IEnumerable<CommonLayer.Models.Transaction> MiniStatement(string accountNo)
        => this.transactionRepository.MiniStatement(accountNo);

        public IEnumerable<CommonLayer.Models.Transaction> CustomizedStatement(CustomizedStatement customizedStatement)
        => this.transactionRepository.CustomizedStatement(customizedStatement);
    }
}
