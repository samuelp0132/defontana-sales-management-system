using SalesManagementSystem.Application.Sales.DTOs;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Sales.Interfaces;

public interface ISalesService
{

    Task<IEnumerable<VentaDetalle>> GetSalesAsync(int days, CancellationToken cancellationToken = default);
    int CalculateTotalSales(IEnumerable<VentaDetalle> sales);
    int CalculateTotalQuantity(IEnumerable<VentaDetalle> sales);
    (int HighestAmount, DateTime DateTimeOfSale) GetHighestAmountSale(IEnumerable<VentaDetalle> sales);
    ProductSalesResultDto GetProductWithHighestTotalSales(IEnumerable<VentaDetalle> sales);
    LocationSalesResultDto GetLocationWithHighestSales(IEnumerable<VentaDetalle> sales);
    BrandProfitMarginResultDto GetBrandWithHighestProfitMargin(IEnumerable<VentaDetalle> sales);
    List<TopSellingProductResultDto> GetTopSellingProductsByLocal(IEnumerable<VentaDetalle> sales);
}