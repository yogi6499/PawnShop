using PawnShop.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PawnShop.Application.Interfaces.IRepositories;

public interface ILoanQueryRepository
{
    Task<IEnumerable<LoanDto>> GetLoansByTenantAsync(Guid tenantId);
    Task<LoanDetailsDto?> GetByIdAsync(Guid tenantId, int id);
    Task<IEnumerable<LoanDto>> GetLoansByCustomerAsync(Guid tenantId, int customerId);
}
