using BankingApplication.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.DataLayer.Contracts
{
    public interface IAccountRepository
    {
        string AddAccount(Account account);

        Account GetAccount(string id, bool isCustomer);

        bool UpdateAccount(Account account, bool isACType);

        bool DeleteAccount(string acNumber);

        Account GetAccount(Account account);

        IEnumerable<Account> GetCustomerAccounts(string customerId);

        bool CurrentAccountValidation(string accountNo,string tin);
    }
}
