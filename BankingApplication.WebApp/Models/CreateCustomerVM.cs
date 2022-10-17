using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApplication.WebApp.Models
{
    public class CreateCustomerVM
    {
        [Display(Name = "Enter First Name")]
        [Required(ErrorMessage = "First Name cannot be blank.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No numeric or special characters allowed")]
        public string FirstName { get; set; }

        [Display(Name = "Enter Last Name")]
        [Required(ErrorMessage = "Last Name cannot be blank.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No numeric or special characters allowed")]
        public string LastName { get; set; }

        [Display(Name = "Choose Gender")]
        [Required(ErrorMessage = "Gender cannot be blank.")]
        public string Gender { get; set; }

        [Display(Name = "Enter DOB")]
        [Required(ErrorMessage = "DOB cannot be blank.")]
        public DateTime Dob { get; set; }

        [Display(Name = "Enter emailId")]
        [Required(ErrorMessage = "EmailId cannot be blank.")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string EmailId { get; set; }

        [Display(Name = "Enter Mobile Number")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Mobile Number cannot be blank and should have 10 digits")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Enter City")]
        [Required(ErrorMessage = "City cannot be blank.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No numeric or special characters allowed")]

        public string City { get; set; }

        [Display(Name = "Enter State")]
        [Required(ErrorMessage = "State cannot be blank.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "No numeric or special characters allowed")]
        public string State { get; set; }

        [Display(Name = "Enter Postal Code")]
        [Required(ErrorMessage = "Postal Code cannot be blank.")]
        [RegularExpression(@"^(\d{6})$", ErrorMessage = "Invalid PIN code")]
        public string Pincode { get; set; }
    }
}
