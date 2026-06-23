using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Domain.Enums
{
    public enum ProfitTransactionType
    {
        InterestReceived = 1,

        PenaltyReceived = 2,

        ProfitWithdrawn = 3,

        ProfitReinvested = 4,

        ProfitAdjustment = 5
    }
}
