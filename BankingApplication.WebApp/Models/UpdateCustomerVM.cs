using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Models
{
    public class UpdateCustomerVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
    }
}
