using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Models
{
    public class LoginVM
    {
        [Display(Name = "Enter Email ID.")]
        [Required(ErrorMessage = "Email Id cannot be blank.")]
        //[EmailAddress]
        public string LoginId { get; set; }

        [Display(Name = "Enter Password")]
        [Required(ErrorMessage = "Password cannot be blank.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
