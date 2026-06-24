using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IUseCases;
using PawnShop.Domain.Entities;
using PawnShop.Domain.Enums;

namespace PawnShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan(
    CreateLoanRequest request)
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
            catch(Exception ex)
            {
                throw; // Let the global exception handler deal with it
            }
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(
    CreatePaymentRequest request)
        {
            try
            {
                var totalAmount =
                            request.PrincipalAmount +
                            request.InterestAmount +
                            request.PenaltyAmount +
                            request.ServiceFee;

                if (totalAmount <= 0)
                {
                    return BadRequest("Invalid payment amount");
                }
                await _loanService.CreatePaymentAsync(request);

                return Created();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                throw; // Let the global exception handler deal with it
            }
            
        }
    }
}
