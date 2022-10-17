using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.EFLayer.Models
{
    public class TokenRequest
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
