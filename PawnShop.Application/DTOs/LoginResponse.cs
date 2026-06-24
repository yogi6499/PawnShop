using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Application.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;

        public DateTime Expiry { get; set; }

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
