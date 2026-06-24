using PawnShop.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IUseCases
{
    public interface ILoanService
    {
        Task<string> CreateLoanAsync(CreateLoanRequest request);
        Task<bool> CreatePaymentAsync(CreatePaymentRequest request);
    }
}
