using BankingApplication.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.BusinessLayer.Contracts
{
    public interface ICustomerManager
    {
        string AddCustomer(Customer customer);

        Customer GetCustomer(string cId,string managerId,bool isEmail = false);

        IEnumerable<Customer> GetCustomers();

        bool DeleteCustomer(string cId);

        bool UpdateCustomer(Customer customer);
    }
}
