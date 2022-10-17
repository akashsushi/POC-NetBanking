using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Models
{
    public class UpdateAccountVM
    {
        public string CustomerId { get; set; }
        public double Balance { get; set; }
        public DateTime DOC { get; set; }
        public string Tin { get; set; }
        public string AccountType { get; set; }
        public string IFSC { get; set; }

    }
}
