using System;
using System.Collections.Generic;

#nullable disable

namespace BankingApplication.EFLayer.Models
{
    public partial class ManagerCustomerRelation
    {
        public string ManagerId { get; set; }
        public string CustomerId { get; set; }
        public int RelationId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Manager Manager { get; set; }
    }
}
