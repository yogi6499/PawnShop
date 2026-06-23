using PawnShop.Domain.Common;
using PawnShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Domain.Entities
{
    public class Loan : TenantEntity
    {
        [Key]
        public int LoanId { get; set; }

        public required string LoanNumber { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public decimal PrincipalAmount { get; set; }

        public decimal InterestPercentage { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime DueDate { get; set; }

        public LoanStatus Status { get; set; }

        public ICollection<GoldItem> GoldItems { get; set; }
            = new List<GoldItem>();

        public ICollection<Payment> Payments { get; set; }
            = new List<Payment>();
    }
}
