using PawnShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Domain.Entities
{
    public class Customer : TenantEntity
    {
        [Key]
        public int CustomerId { get; set; }

        public required string Name { get; set; }

        public required string Phone { get; set; }

        public string? AadhaarNumber { get; set; }

        public string? Address { get; set; }

        public string? Notes { get; set; }

        public ICollection<Loan> Loans { get; set; }
            = new List<Loan>();
    }
}
