using PawnShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Domain.Entities
{
    public class CapitalContributor : TenantEntity
    {
        [Key]
        public int CapitalContributorId { get; set; }

        public required string Name { get; set; }

        public string? Phone { get; set; }

        public bool IsActive { get; set; }
    }
}
