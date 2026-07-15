using System;

namespace PawnShop.Application.DTOs;

public record TenantDto
{
    public Guid TenantId { get; init; }
    public string Code { get; init; } = null!;
    public string Name { get; init; } = null!;
}
