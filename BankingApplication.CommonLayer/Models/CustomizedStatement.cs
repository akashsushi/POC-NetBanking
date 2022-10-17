using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.CommonLayer.Models
{
    public class CustomizedStatement
    {
        public string AccountNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NumberOfTransaction { get; set; }
        public double LowerLimit { get; set; }
    }
}
