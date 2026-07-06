using PawnShop.Domain.Enums;
using System;

namespace PawnShop.Application.DTOs;

public class LoanDto
{
    public int LoanId { get; set; }
    public string LoanNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal PrincipalAmount { get; set; }
    public decimal InterestPercentage { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public LoanStatus Status { get; set; }
}
