using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Sales.DTOs;

public class TopSellingProductResultDto
{
    public Producto Product { get; set; }
    public int TotalQuantitySold { get; set; }
}

public class TopSellingProductByLocal
{
    public Local Local { get; set; }
    public TopSellingProductResultDto TopSellingProduct { get; set; }
}