using System;
using System.Collections.Generic;

#nullable disable

namespace BankingApplication.EFLayer.Models
{
    public partial class CorporatetAccount
    {
        public int Ind { get; set; }
        public double WithdrawlLimit { get; set; }
        public double MinimumBalance { get; set; }
    }
}
