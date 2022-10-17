using BankingApplication.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankingApplication.BusinessLayer.Contracts
{
    public interface ITransactionManager
    {
        int Transfer(Transaction transaction);

        int Withdrawal(Transaction transaction);

        int Deposit(Transaction transaction);

        IEnumerable<CommonLayer.Models.Transaction> MiniStatement(string accountNo);

        IEnumerable<CommonLayer.Models.Transaction> CustomizedStatement(CustomizedStatement customizedStatement);
    }
}
