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
        private readonly ILoanQueryRepository _loanQueryRepository;

        public LoanService(
            ILoanCommandRepository loanCommandRepository,
            ILoanQueryRepository loanQueryRepository)
        {
            _loanCommandRepository = loanCommandRepository;
            _loanQueryRepository = loanQueryRepository;
        }

        public async Task<string> CreateLoanAsync(CreateLoanRequest request)
        {
            return await _loanCommandRepository.CreateLoanAsync(request);
        }

        public async Task<bool> CreatePaymentAsync(CreatePaymentRequest request)
        {
            return await _loanCommandRepository.CreatePaymentAsync(request);
        }

        public async Task<IEnumerable<LoanDto>> GetLoansByTenantAsync(Guid tenantId)
        {
            return await _loanQueryRepository.GetLoansByTenantAsync(tenantId);
        }

        public async Task<LoanDetailsDto?> GetByIdAsync(Guid tenantId, int id)
        {
            return await _loanQueryRepository.GetByIdAsync(tenantId, id);
        }

        public async Task<IEnumerable<LoanDto>> GetLoansByCustomerAsync(Guid tenantId, int customerId)
        {
            return await _loanQueryRepository.GetLoansByCustomerAsync(tenantId, customerId);
        }
    }
}
