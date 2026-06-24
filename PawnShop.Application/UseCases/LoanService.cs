using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Application.Interfaces.IUseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Application.UseCases
{
    public class LoanService : ILoanService
    {
        private readonly ILoanCommandRepository _loanCommandRepository;

        public LoanService(ILoanCommandRepository loanCommandRepository)
        {
            _loanCommandRepository = loanCommandRepository;
        }

        public async Task<string> CreateLoanAsync(CreateLoanRequest request)
        {
            return await _loanCommandRepository.CreateLoanAsync(request);
        }

        public async Task<bool> CreatePaymentAsync(CreatePaymentRequest request)
        {
            return await _loanCommandRepository.CreatePaymentAsync(request);
        }
    }
}
