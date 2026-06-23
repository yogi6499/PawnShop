using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Domain.Enums
{
    public enum LoanStatus
    {
        Active = 1,

        Closed = 2,

        Overdue = 3,

        Auctioned = 4,

        Cancelled = 5
    }
}
