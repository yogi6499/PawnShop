using System;

namespace PawnShop.Application.DTOs;

public class AddCapitalRequest
{
    public int? CapitalContributorId { get; set; }
    public decimal Amount { get; set; }
    public string? Remarks { get; set; }
}

public class WithdrawCapitalRequest
{
    public decimal Amount { get; set; }
    public string? Remarks { get; set; }
}

public class CapitalContributorDto
{
    public int CapitalContributorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public Guid TenantId { get; set; }
}
