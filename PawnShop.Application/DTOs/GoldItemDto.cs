using System;

namespace PawnShop.Application.DTOs;

public class GoldItemDto
{
    public int GoldItemId { get; set; }
    public int ItemType { get; set; }
    public decimal Weight { get; set; }
    public decimal Purity { get; set; }
    public string? Description { get; set; }
}
