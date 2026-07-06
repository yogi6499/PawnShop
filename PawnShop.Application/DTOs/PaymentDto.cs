using PawnShop.Domain.Enums;
using System;

namespace PawnShop.Application.DTOs;

public class PaymentDto
{
    public int PaymentId { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal PenaltyAmount { get; set; }
    public decimal ServiceFee { get; set; }
    public decimal TotalAmount { get; set; }
    public TransactionType PaymentType { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Remarks { get; set; }
}
