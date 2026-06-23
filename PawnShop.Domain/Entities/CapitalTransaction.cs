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
    public class CapitalTransaction : TenantEntity
    {
        [Key]
        public int CapitalTransactionId { get; set; }

        public CapitalTransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public decimal BalanceAfterTransaction { get; set; }

        public int? CapitalContributorId { get; set; }

        public CapitalContributor? CapitalContributor { get; set; }

        public int? LoanId { get; set; }

        public Loan? Loan { get; set; }

        public int? PaymentId { get; set; }

        public Payment? Payment { get; set; }

        public string? Remarks { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
