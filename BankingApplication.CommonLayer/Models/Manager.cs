using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.CommonLayer.Models
{
    public class Manager
    {
        public string ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string ManagerPassword { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }

        private List<Customer> customers = null;

        /// <summary>
        /// Method to add Customer to the list
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            this.customers.Add(customer);
        }

        /// <summary>
        /// Method to get list of customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return this.customers;
        }
    }
}
