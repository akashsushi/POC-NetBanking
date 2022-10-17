using BankingApplication.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.DataLayer.Contracts
{
    public interface ICustomerRepository
    {
        string AddCustomer(Customer customer);

        Customer GetCustomer(string cId,string managerId, bool isEmail);

        IEnumerable<Customer> GetCustomers();

        bool UpdateCustomer(Customer customer);

        bool DeleteCustomer(string cId);
    }
}
