using System.Collections.Generic;

namespace PawnShop.Application.DTOs;

public class LoanDetailsDto
{
    public int LoanId { get; set; }
    public string LoanNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal PrincipalAmount { get; set; }
    public decimal InterestPercentage { get; set; }
    public System.DateTime LoanDate { get; set; }
    public System.DateTime DueDate { get; set; }
    public PawnShop.Domain.Enums.LoanStatus Status { get; set; }
    public List<GoldItemDto> GoldItems { get; set; } = new();
    public List<PaymentDto> Payments { get; set; } = new();
}
