using PawnShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Application.DTOs
{
    public class CreatePaymentRequest
    {
        public int LoanId { get; set; }
        public Guid TenantId { get; set; }

        public decimal PrincipalAmount { get; set; }

        public decimal InterestAmount { get; set; }

        public decimal PenaltyAmount { get; set; }

        public decimal ServiceFee { get; set; }

        public TransactionType TransactionType { get; set; }
        public bool CloseLoan { get; set; }

        public string? Remarks { get; set; }
    }
}
