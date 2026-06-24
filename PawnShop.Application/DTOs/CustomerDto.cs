
namespace PawnShop.Application.DTOs;

public record CustomerDto
{
    public int CustomerId { get; init; }
    public string Name { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public string? AadhaarNumber { get; init; }
    public string? Address { get; init; }
    public string? Notes { get; init; }
    public Guid TenantId { get; init; }
}