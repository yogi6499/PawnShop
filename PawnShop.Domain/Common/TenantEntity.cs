using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Domain.Common
{
    public abstract class TenantEntity : BaseEntity
    {
        public Guid TenantId { get; set; }
    }
}
