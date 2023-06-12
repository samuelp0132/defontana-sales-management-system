using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Sales.DTOs;

public class ProductSalesResultDto
{
    public string ProductName { get; set; }
    public int TotalSalesAmount { get; set; }
}