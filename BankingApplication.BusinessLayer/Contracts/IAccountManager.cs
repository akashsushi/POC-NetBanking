using BankingApplication.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.BusinessLayer.Contracts
{
    public interface IAccountManager
    {
        string AddAccount(Account account);

        Account GetAccount(string id,bool isCustomerId = false);

        Account GetAccount(Account account);

        IEnumerable<Account> GetCustomerAccounts(string customerId);

        bool CurrentAccountValidation(string accountNo, string tin);

        bool UpdateAccount(Account account,bool isACType = false);

        bool DeleteAccount(string acNumber);
    }
}
