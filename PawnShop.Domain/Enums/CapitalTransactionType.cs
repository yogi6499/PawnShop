using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Domain.Enums
{
    public enum CapitalTransactionType
    {
        CapitalAdded = 1,

        LoanIssued = 2,

        PrincipalReceived = 3,

        CapitalWithdrawn = 4,

        CapitalAdjustment = 5
    }
}
