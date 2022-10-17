using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Models
{
    public class FundTransferVM
    {
        [Display(Name = "Enter the Source Account No:")]
        [Required(ErrorMessage = "Source Account should not be blank!")]
        public string SourceAccountNo { get; set; }
        [Display(Name = "Enter the Amount to be transfered")]
        [Required(ErrorMessage = "Transfer amount should not be blank!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid amount")]
        public double TransactionAmount { get; set; }
        [Display(Name = "Choose the transaction type ")]
        [Required(ErrorMessage = "Transaction type should not be blank!")]
        public string TransactionType { get; set; }
        [Display(Name = "Enter the Destination Account:")]
        [Required(ErrorMessage = "Destination Account should not be blank!")]
        public string DestinationAccountNo { get; set; }
        [Display(Name = "Enter the Transaction Description:")]
        [Required(ErrorMessage = "Description should not be blank!")]
        public string TransactionDescription { get; set; }
    }
}
