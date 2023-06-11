using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesManagementSystem.Application.Common.Utils;
using SalesManagementSystem.Application.Sales.Interfaces;
using SalesManagementSystem.Application.Sales.Services;
using SalesManagementSystem.Infrastructure.CrossCutting;
using SalesManagementSystem.Infrastructure.ServiceExtension;

// Configuration setup
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

// Register your services
services.AddDatabaseServices(configuration);
services.AddRegistration();

// Register SalesService
services.AddTransient<ISalesService, SalesService>();

// Build the service provider
var serviceProvider = services.BuildServiceProvider();
var SalesService = serviceProvider.GetRequiredService<ISalesService>();

// Get sales data
var salesLast30Days = await SalesService.GetSalesAsync();

// Calculate total sales amount and quantity
var totalSales = salesLast30Days.Sum(record => record.TotalLinea);
var quantityTotalSales = salesLast30Days.Sum(record => record.Cantidad);


// Find the highest amount sale
var highestAmountSale = salesLast30Days.MaxBy(record => record.Venta.Total);
var highestAmount = highestAmountSale.Venta.Total;
var dateTimeOfSale = highestAmountSale.Venta.Fecha;

// Find the product with the highest sales
var productWithHighestTotalSales = salesLast30Days
    .GroupBy(record => record.Producto.Nombre)
    .Select(group => new
    {
        ProductName = group.Key,
        TotalSalesAmount = group.Sum(record => record.TotalLinea)
    }).MaxBy(record => record.TotalSalesAmount);

var productName = productWithHighestTotalSales.ProductName;
var totalSalesAmount = productWithHighestTotalSales.TotalSalesAmount;

// Find the location with the highest amount of sales
var locationWithHighestSales = salesLast30Days
    .GroupBy(record => record.Venta.Local.Nombre)
    .Select(group => new
    {
        Location = group.Key,
        TotalSalesAmount = group.Sum(record => record.Venta.Total)
    }).MaxBy(group => group.TotalSalesAmount);

var localName = locationWithHighestSales.Location;
var totalLocalSalesAmount = locationWithHighestSales.TotalSalesAmount;

// Find the brand with the highest profit margin
var brandWithHighestProfitMargin = salesLast30Days
    .GroupBy(s => s.Producto.Marca)
    .Select(g => new
    {
        Brand = g.Key,
        AverageProfitMargin = g.Average(s => (decimal)((s.PrecioUnitario * s.Cantidad) - s.Producto.CostoUnitario) / (s.PrecioUnitario * s.Cantidad)) * 100
    })
    .ToList();

var brandWithHighestMargin = brandWithHighestProfitMargin.OrderByDescending(b => b.AverageProfitMargin).FirstOrDefault();


var brandName = brandWithHighestMargin.Brand.Nombre;
var profitMargin = brandWithHighestMargin.AverageProfitMargin;

// Find the top-selling products by local
var topSellingProductsByLocal = salesLast30Days
    .GroupBy(vd => new {vd.Venta.Local.Nombre, vd.Venta.Total})
    .Select(group => new
    {
        Local = group.Key,
        TopSellingProduct = group.MaxBy(vd => vd.Venta.Total)?.Producto
    });

 // Output the results
Console.WriteLine($"Monto total del mes: {CurrencyConverter.ConvertToCurrencyString(totalSales)}, Cantidad Total : {CurrencyConverter.ConvertToCurrencyString(quantityTotalSales)}");
Console.WriteLine($"Fecha venta mas alta: {dateTimeOfSale}, Monto : {CurrencyConverter.ConvertToCurrencyString(highestAmount)}");
Console.WriteLine($"Producto con mas venta: {productName}, Monto : {CurrencyConverter.ConvertToCurrencyString(totalSalesAmount)}");
Console.WriteLine($"Local con mas ventas : {localName}, Ventas : {CurrencyConverter.ConvertToCurrencyString(totalLocalSalesAmount)}");
Console.WriteLine($"Marca con mayor margen de ganancias : {brandName}, Margen : {profitMargin}");
foreach (var product in topSellingProductsByLocal)
{
    Console.WriteLine($"Producto: {product.TopSellingProduct.Nombre}, Local: {product.Local.Nombre}, Costo: {CurrencyConverter.ConvertToCurrencyString(product.TopSellingProduct.CostoUnitario)}");
}
