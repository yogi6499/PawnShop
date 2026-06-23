using PawnShop.Domain.Common;
using PawnShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Domain.Entities
{
    public class Payment : TenantEntity
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey(nameof(Loan))]
        public int LoanId { get; set; }

        public Loan Loan { get; set; } = null!;

        public decimal Amount { get; set; }

        public TransactionType PaymentType { get; set; }

        public DateTime PaymentDate { get; set; }

        public string? Remarks { get; set; }
    }
}
