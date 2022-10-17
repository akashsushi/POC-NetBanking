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
    public class CustomerAsyncManager : ICustomerAsyncManager
    {
        private readonly ICustomerAsyncRepository customerRepository;

        public CustomerAsyncManager(ICustomerAsyncRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public Task<IEnumerable<Transaction>> CustomizedStatement(CustomizedStatement customizedStatement)
        => this.customerRepository.CustomizedStatement(customizedStatement);

        public Task<Account> GetBalance(string id)
        => this.customerRepository.GetBalance(id);

        public Task<IEnumerable<Transaction>> MiniStatement(string accountNo)
        => this.customerRepository.MiniStatement(accountNo);

        public Task<int> Transfer(Transaction transaction)
        => this.customerRepository.Transfer(transaction);
    }
}
