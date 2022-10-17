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
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public string AddCustomer(Customer customer)
        => this.customerRepository.AddCustomer(customer);

        public bool DeleteCustomer(string cId)
        => this.customerRepository.DeleteCustomer(cId);

        public Customer GetCustomer(string cId, string managerId, bool isEmail = false)
        => this.customerRepository.GetCustomer(cId,managerId, isEmail);

        public IEnumerable<Customer> GetCustomers()
        => this.customerRepository.GetCustomers();

        public bool UpdateCustomer(Customer customer)
        => this.customerRepository.UpdateCustomer(customer);
    }
}
