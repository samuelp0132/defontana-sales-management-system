using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Sales.DTOs;

public class TopSellingProductResultDto
{
    public string LocalName { get; set; }
    public string ProductName { get; set; }
    public int TotalQuantitySold { get; set; }
}