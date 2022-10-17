using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.CommonLayer.Models
{
    public class CorporateAccount
    {
        public int Ind { get; set; }
        public double WithdrawlLimit { get; set; }
        public double MinimumBalance { get; set; }
    }
}
