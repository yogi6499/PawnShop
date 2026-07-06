using System;

namespace PawnShop.Application.DTOs;

public class DashboardDto
{
    public decimal? AvailableCapital { get; set; }
    public decimal? AvailableProfit { get; set; }
    public decimal MoneyOnLoan { get; set; }
    public int ActiveLoans { get; set; }
    public int ClosedLoans { get; set; }
    public int Customers { get; set; }
}
