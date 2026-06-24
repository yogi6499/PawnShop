using PawnShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IUseCases
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
