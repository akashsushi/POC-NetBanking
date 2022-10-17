using BankingApplication.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.DataLayer.Contracts
{
    public interface ICustomerAsyncRepository
    {
        Task<CommonLayer.Models.Account> GetBalance(string id);

        Task<int> Transfer(CommonLayer.Models.Transaction transaction);

        Task<IEnumerable<CommonLayer.Models.Transaction>> MiniStatement(string accountNo);

        Task<IEnumerable<CommonLayer.Models.Transaction>> CustomizedStatement(CustomizedStatement customizedStatement);

    }
}
