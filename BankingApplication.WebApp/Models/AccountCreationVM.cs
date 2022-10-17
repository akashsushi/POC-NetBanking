using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Models
{
    public class AccountCreationVM
    {
        [Display(Name = "Enter Your customerId ")]
        [Required(ErrorMessage = "CustomerId should not be blank!")]
        public string CustomerId { get; set; }

        [Display(Name = "Enter Your Balance ")]
        [Required(ErrorMessage = "Balance should not be blank!")]
        public double Balance { get; set; }

        [Display(Name = "Enter Your PAN(For SavingsA/C & Corporate A/C) and TIN(For Courrent A/C)")]
        [Required(ErrorMessage = "TIN should not be blank !")]
        public string Tin { get; set; }

        [Display(Name = "Choose Your Account Type ")]
        [Required(ErrorMessage = "Account Type Should not be blank!")]
        public string AccountType { get; set; }

        [Display(Name = "Enter Your IFSC ")]
        [Required(ErrorMessage = "Balance should not be blank!")]
        public string IFSC { get; set; }

    }
}
