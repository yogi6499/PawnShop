using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IUseCases;
using System;
using System.Threading.Tasks;

namespace PawnShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan(CreateLoanRequest request)
        {
            try
            {
                var loanNumber = await _loanService.CreateLoanAsync(request);
                return Ok(new { LoanNumber = loanNumber });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(CreatePaymentRequest request)
        {
            try
            {
                var totalAmount =
                    request.PrincipalAmount +
                    request.InterestAmount +
                    request.PenaltyAmount +
                    request.ServiceFee;

                if (totalAmount <= 0)
                    return BadRequest("Invalid payment amount");

                await _loanService.CreatePaymentAsync(request);
                return Created();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLoans/{tenantId}")]
        public async Task<IActionResult> GetLoans(Guid tenantId)
        {
            var loans = await _loanService.GetLoansByTenantAsync(tenantId);
            return Ok(loans);
        }

        [HttpGet("GetById/{tenantId}/{id}")]
        public async Task<IActionResult> GetById(Guid tenantId, int id)
        {
            var loan = await _loanService.GetByIdAsync(tenantId, id);
            return loan is null ? NotFound() : Ok(loan);
        }

        [HttpGet("GetLoanHistory/{tenantId}/{customerId}")]
        public async Task<IActionResult> GetLoanHistory(Guid tenantId, int customerId)
        {
            var loans = await _loanService.GetLoansByCustomerAsync(tenantId, customerId);
            return Ok(loans);
        }
    }
}
