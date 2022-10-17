using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Models
{
    public class BalanceEnquiry
    {
        public string AccountNo { get; set; }
        public string FirstName { get; set; }
        public string Balance { get; set; }
    }
}
