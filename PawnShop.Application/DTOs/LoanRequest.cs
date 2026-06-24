using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Application.DTOs
{
    public class CreateLoanRequest
    {
        public int CustomerId { get; set; }
        public Guid TenantId { get; set; }

        public decimal PrincipalAmount { get; set; }

        public decimal InterestPercentage { get; set; }

        public DateTime MaturityDate { get; set; }

        public string? Notes { get; set; }

        public List<GoldItemRequest> GoldItems { get; set; } = [];
    }

    public class GoldItemRequest
    {
        public int ItemType { get; set; }

        public decimal Weight { get; set; }

        public decimal Purity { get; set; }

        public string? Description { get; set; }
    }
}



