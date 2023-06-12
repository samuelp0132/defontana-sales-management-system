using SalesManagementSystem.Application.Sales.DTOs;
using SalesManagementSystem.Application.Sales.Interfaces;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Infrastructure.Repositories.UnitOfWork.Interfaces;

namespace SalesManagementSystem.Application.Sales.Services
{
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
            DateTimeOffset startDate = DateTimeOffset.Now.AddDays(-days);
            DateTimeOffset endDate = DateTimeOffset.Now;

            return await _unitOfWork.VentaDetalleRepository.GetAll(
                orderBy: q => q.OrderByDescending(c => c.Id),
                filter: v => v.Venta.Fecha >= startDate && v.Venta.Fecha <= endDate,
                includeProperties: $"{nameof(Producto)}.{nameof(Marca)},{nameof(Producto)},{nameof(Venta)},{nameof(Venta)}.{nameof(Local)}"
            );
        }

        /// <summary>
        /// Calculates the total sales amount from the given sales.
        /// </summary>
        /// <param name="sales">The sales collection.</param>
        /// <returns>The total sales amount.</returns>
        public int CalculateTotalSales(IEnumerable<VentaDetalle> sales)
        {
            return sales.Sum(record => record.TotalLinea);
        }

        /// <summary>
        /// Calculates the total quantity of items sold from the given sales.
        /// </summary>
        /// <param name="sales">The sales collection.</param>
        /// <returns>The total quantity sold.</returns>
        public int CalculateTotalQuantity(IEnumerable<VentaDetalle> sales)
        {
            return sales.Sum(record => record.Cantidad);
        }

        /// <summary>
        /// Gets the highest amount sale from the given sales.
        /// </summary>
        /// <param name="sales">The sales collection.</param>
        /// <returns>The highest amount sale and its associated date and time.</returns>
        public (int HighestAmount, DateTime DateTimeOfSale) GetHighestAmountSale(IEnumerable<VentaDetalle> sales)
        {
            var highestAmountSale = sales.MaxBy(record => record.Venta.Total);
            var highestAmount = highestAmountSale?.Venta.Total ?? 0;
            var dateTimeOfSale = highestAmountSale?.Venta.Fecha ?? DateTime.MinValue;

            return (highestAmount, dateTimeOfSale);
        }

        /// <summary>
        /// Gets the product with the highest total sales from the given sales.
        /// </summary>
        /// <param name="sales">The sales collection.</param>
        /// <returns>The product with the highest total sales.</returns>
        public ProductSalesResultDto GetProductWithHighestTotalSales(IEnumerable<VentaDetalle> sales)
        {
            var productWithHighestTotalSales = sales
                .GroupBy(record => record.Producto.Nombre)
                .Select(group => new
                {
                    ProductName = group.Key,
                    TotalSalesAmount = group.Sum(record => record.TotalLinea)
                })
                .MaxBy(record => record.TotalSalesAmount);

            return new ProductSalesResultDto
            {
                ProductName = productWithHighestTotalSales.ProductName,
                TotalSalesAmount = productWithHighestTotalSales.TotalSalesAmount
            };
        }

        /// <summary>
        /// Gets the location with the highest sales from the given sales.
        /// </summary>
        /// <param name="sales">The sales collection.</param>
        /// <returns>The location with the highest sales.</returns>
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

        /// <summary>
        /// Gets the brand with the highest profit margin from the given sales.
        /// </summary>
        /// <param name="sales">The sales collection.</param>
        /// <returns>The brand with the highest profit margin.</returns>
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
                .OrderByDescending(b => b.AverageProfitMargin)
                .FirstOrDefault();

            return new BrandProfitMarginResultDto
            {
                BrandName = brandWithHighestProfitMargin.Brand.Nombre,
                AverageProfitMargin = brandWithHighestProfitMargin.AverageProfitMargin
            };
        }

        /// <summary>
        /// Gets the top selling products by local from the given sales.
        /// </summary>
        /// <param name="sales">The sales collection.</param>
        /// <returns>A list of top selling products by local.</returns>
        public List<TopSellingProductResultDto> GetTopSellingProductsByLocal(IEnumerable<VentaDetalle> sales)
        {
            var mostSoldProductsByLocal = sales
                .GroupBy(sale => sale.Venta.Local)
                .Select(group => new
                {
                    Local = group.Key,
                    ProductSales = group.GroupBy(sale => sale.IdProducto)
                        .Select(productGroup => new
                        {
                            Product = productGroup.First().Producto,
                            TotalQuantitySold = productGroup.Sum(sale => sale.Cantidad)
                        })
                })
                .SelectMany(group => group.ProductSales.OrderByDescending(product => product.TotalQuantitySold)
                    .Take(1)
                    .Select(product => new TopSellingProductResultDto
                    {
                        LocalName = group.Local.Nombre,
                        ProductName = product.Product.Nombre,
                        TotalQuantitySold = product.TotalQuantitySold
                    }))
                .OrderBy(result => result.LocalName)
                .ToList();

            return mostSoldProductsByLocal;
        }
    }
}
