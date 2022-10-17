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
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository accountRepository;

        public AccountManager(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public string AddAccount(Account account)
        => this.accountRepository.AddAccount(account);

        public bool DeleteAccount(string acNumber)
        => this.accountRepository.DeleteAccount(acNumber);

        public Account GetAccount(string id, bool isCustomerId = false)
        => this.accountRepository.GetAccount(id,isCustomerId);

        public bool UpdateAccount(Account account, bool isACType = false)
        => this.accountRepository.UpdateAccount(account,isACType);

        public Account GetAccount(Account account)
        => this.accountRepository.GetAccount(account);

        public IEnumerable<Account> GetCustomerAccounts(string customerId)
        => this.accountRepository.GetCustomerAccounts(customerId);

        public bool CurrentAccountValidation(string accountNo, string tin)
        => this.accountRepository.CurrentAccountValidation(accountNo, tin);
    }
}
