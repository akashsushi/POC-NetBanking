using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Models
{
    public class CustomizedStatementVM
    {
        [Display(Name = "Enter Your AccountNo :")]
        [Required(ErrorMessage = "AccountNo should not be blank!")]
        public string AccountNo { get; set; }

        [Display(Name = "Choose From Date :")]
        [Required(ErrorMessage = "From Date should not be blank!")]
        public DateTime FromDate { get; set; }

        [Display(Name = "Choose To Date :")]
        [Required(ErrorMessage = "To Date should not be blank!")]
        public DateTime ToDate { get; set; }

        [Display(Name = "Enter Number of  Transactions to fetch :")]
        [Required(ErrorMessage = "Number of  Transactions should not be blank!")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Number of Transactions")]
        public int NumberOfTransaction { get; set; }

        [Display(Name = "Enter Your Lower Limit ")]
        [Required(ErrorMessage = "Lower Limit should not be blank!")]
        public double LowerLimit { get; set; }
    }
}
