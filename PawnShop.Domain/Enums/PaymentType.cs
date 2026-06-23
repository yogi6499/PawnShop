using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Domain.Enums
{
    public enum TransactionType
    {
        LoanIssued = 1,
        InterestReceived = 2,
        PrincipalReceived = 3,
        LoanClosed = 4,
        Adjustment = 5
    }
}
