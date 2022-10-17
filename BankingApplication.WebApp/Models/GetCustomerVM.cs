using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Models
{
    public class GetCustomerVM
    {
        [Display(Name = "Enter Customer ID.")]
        [Required(ErrorMessage = "Customer Id cannot be blank.")]
        public string CustomerId { get; set; }
    }
}
