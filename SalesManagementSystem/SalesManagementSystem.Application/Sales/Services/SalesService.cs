using SalesManagementSystem.Application.Sales.DTOs;
using SalesManagementSystem.Application.Sales.Interfaces;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Infrastructure.Repositories.UnitOfWork.Interfaces;

namespace SalesManagementSystem.Application.Sales.Services;

/// <summary>
/// Service class that provides operations related to sales.
/// </summary>
public class SalesService : ISalesService
{
    private readonly ISalesManagementUnitOfWork _unitOfWork;
    /// <summary>
    /// Initializes a new instance of the SalesService class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work instance to be used by the service.</param>
    public SalesService(ISalesManagementUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    /// <summary>
    /// Retrieves the sales details within a specified date range.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A collection of VentaDetalle objects representing the sales details.</returns>
    public async Task<IEnumerable<VentaDetalle>> GetSalesAsync(int days, CancellationToken cancellationToken = default)
    {
        // Set date range
        DateTimeOffset startDate = DateTimeOffset.Now.AddDays(-30);
        DateTimeOffset endDate = DateTimeOffset.Now;
        
        return await _unitOfWork.VentaDetalleRepository.GetAll(
            orderBy: q => q.OrderByDescending(c => c.Id),
            filter: v => v.Venta.Fecha >= startDate && v.Venta.Fecha <= endDate,
            includeProperties: $"{nameof(Producto)}.{nameof(Marca)},{nameof(Producto)},{nameof(Venta)},{nameof(Venta)}.{nameof(Local)}"
            );
    }

    public int CalculateTotalSales(IEnumerable<VentaDetalle> sales)
    {
        return sales.Sum(record => record.TotalLinea);
    }

    public int CalculateTotalQuantity(IEnumerable<VentaDetalle> sales)
    {
        return sales.Sum(record => record.Cantidad);
    }

    public (int HighestAmount, DateTime DateTimeOfSale) GetHighestAmountSale(IEnumerable<VentaDetalle> sales)
    {
        var highestAmountSale = sales.MaxBy(record => record.Venta.Total);
        var highestAmount = highestAmountSale?.Venta.Total ?? 0;
        var dateTimeOfSale = highestAmountSale?.Venta.Fecha ?? DateTime.MinValue;

        return (highestAmount, dateTimeOfSale);
    }   


    public ProductSalesResultDto GetProductWithHighestTotalSales(IEnumerable<VentaDetalle> sales)
    {
        var productWithHighestTotalSales = sales
            .GroupBy(record => record.Producto.Nombre)
            .Select(group => new
            {
                ProductName = group.Key,
                TotalSalesAmount = group.Sum(record => record.TotalLinea)
            }).MaxBy(record => record.TotalSalesAmount);

        return new ProductSalesResultDto
        {
            ProductName = productWithHighestTotalSales.ProductName,
            TotalSalesAmount = productWithHighestTotalSales.TotalSalesAmount
        };
    }

    public LocationSalesResultDto GetLocationWithHighestSales(IEnumerable<VentaDetalle> sales)
    {
        var locationWithHighestSales = sales
            .GroupBy(record => record.Venta.Local)
            .Select(group => new
            {
                Location = group.Key,
                TotalSalesAmount = group.Sum(record => record.Venta.Total)
            })
            .OrderByDescending(group => group.TotalSalesAmount)
            .FirstOrDefault();

        return new LocationSalesResultDto
        {
            LocationName = locationWithHighestSales.Location.Nombre,
            TotalSalesAmount = locationWithHighestSales.TotalSalesAmount
        };
    }

    public BrandProfitMarginResultDto GetBrandWithHighestProfitMargin(IEnumerable<VentaDetalle> sales)
    {
        var brandWithHighestProfitMargin = sales
            .GroupBy(s => s.Producto.Marca)
            .Select(g => new
            {
                Brand = g.Key,
                AverageProfitMargin = g.Average(s => (decimal)((s.PrecioUnitario * s.Cantidad) - s.Producto.CostoUnitario) / (s.PrecioUnitario * s.Cantidad)) * 100
            })
            .ToList()
            .OrderByDescending(b => b.AverageProfitMargin).FirstOrDefault();

        return new BrandProfitMarginResultDto
        {
            BrandName = brandWithHighestProfitMargin.Brand.Nombre,
            AverageProfitMargin = brandWithHighestProfitMargin.AverageProfitMargin
            
        };
    }

    /*
    public List<TopSellingProductByLocal> GetTopSellingProductsByLocal(IEnumerable<VentaDetalle> sales)
    {
        var topSellingProductsByLocal = sales
            .GroupBy(sale => sale.Venta.Local)
            .Select(group => new
            {
                Local = group.Key,
                TopSellingProduct = group.GroupBy(sale => sale.IdProducto)
                    .Select(productGroup => new
                    {
                        Product = productGroup.First().Producto,
                        TotalQuantitySold = productGroup.Sum(sale => sale.Cantidad)
                    }).MaxBy(productGroup => productGroup.TotalQuantitySold)?.Product
            })
            .ToList();

        // Explicitly map the query result to the DTO class
        return topSellingProductsByLocal
            .Select(result => new TopSellingProductByLocal
            {
                Local = result.Local,
                TopSellingProduct = new TopSellingProductResultDto
                {
                    Product = result.TopSellingProduct,
                    TotalQuantitySold = result.TopSellingProduct != null ? result.TopSellingProduct.TotalQuantitySold : 0
                }
            })
            .ToList();
    }
    */
}