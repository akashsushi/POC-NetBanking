using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.CommonLayer.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public bool IsDeleted { get; set; }

        public Manager Manager { get; set; }
    }
}
