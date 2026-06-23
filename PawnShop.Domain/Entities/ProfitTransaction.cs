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
    public class ProfitTransaction : TenantEntity
    {
        [Key]
        public int ProfitTransactionId { get; set; }

        public ProfitTransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public decimal BalanceAfterTransaction { get; set; }

        public int? PaymentId { get; set; }

        public Payment? Payment { get; set; }

        public string? Remarks { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
