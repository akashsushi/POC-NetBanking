using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.CommonLayer.Models
{
    public class SavingsAccount
    {
        public int Ind { get; set; }
        public double WithdrawlLimit { get; set; }
        public double MinimumBalance { get; set; }

        //public string SavingsAccountNo { get; set; }
        //public int AccountNumber { get; set; }
        //public double WithdrawlLimit { get; set; }
        //public double MinimumBalance { get; set; }

        //public Account AccountNumberNavigation { get; set; }
    }
}
