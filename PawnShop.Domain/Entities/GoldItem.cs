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
    public class GoldItem : TenantEntity
    {
        [Key]
        public int GoldItemId { get; set; }

        public GoldItemType ItemType { get; set; }

        public decimal Weight { get; set; }

        public decimal Purity { get; set; }

        public string? Description { get; set; }

        public int LoanId { get; set; }

        public Loan Loan { get; set; } = null!;
    }
}
