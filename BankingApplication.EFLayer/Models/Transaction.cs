using System;
using System.Collections.Generic;

#nullable disable

namespace BankingApplication.EFLayer.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public string SourceAccountNo { get; set; }
        public double TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string DestinationAccountNo { get; set; }
        public string TransactionDescription { get; set; }

        public virtual Account SourceAccountNoNavigation { get; set; }
    }
}
