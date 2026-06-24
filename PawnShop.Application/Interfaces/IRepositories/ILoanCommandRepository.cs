using PawnShop.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IRepositories
{
    public interface ILoanCommandRepository
    {
        Task<string> CreateLoanAsync(CreateLoanRequest request);
        Task<bool> CreatePaymentAsync(CreatePaymentRequest request);
    }
}
