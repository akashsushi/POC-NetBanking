using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Models
{
    public class DeleteAccountVM
    {
        [Display(Name = "Choose confirmation for account deletion? ")]
        public string IsDelete { get; set; }
    }
}
