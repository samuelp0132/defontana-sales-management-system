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

// Get sales of last 30 days
var sales = await SalesService.GetSalesAsync(30);
var totalSales = SalesService.CalculateTotalSales(sales);
var quantityTotalSales = SalesService.CalculateTotalQuantity(sales);
var highestSale = SalesService.GetHighestAmountSale(sales);
var ProductWithHighestTotalSales = SalesService.GetProductWithHighestTotalSales(sales);
var LocationWithHighestSales = SalesService.GetLocationWithHighestSales(sales);
var BrandWithHighestProfitMargin = SalesService.GetBrandWithHighestProfitMargin(sales);
//var TopSellingProductsByLocal = SalesService.GetTopSellingProductsByLocal(salesLast30Days);


// Output the results
Console.WriteLine($"Monto total del mes: {CurrencyConverter.ConvertToCurrencyString(totalSales)}, Cantidad Total : {CurrencyConverter.ConvertToCurrencyString(quantityTotalSales)}");
Console.WriteLine($"Fecha venta mas alta: {highestSale.DateTimeOfSale}, Monto : {CurrencyConverter.ConvertToCurrencyString(highestSale.HighestAmount)}");
Console.WriteLine($"Producto con mas venta: {ProductWithHighestTotalSales.ProductName}, Monto : {CurrencyConverter.ConvertToCurrencyString(ProductWithHighestTotalSales.TotalSalesAmount)}");
Console.WriteLine($"Local con mas ventas : {LocationWithHighestSales.LocationName}, Ventas : {CurrencyConverter.ConvertToCurrencyString(LocationWithHighestSales.TotalSalesAmount)}");
Console.WriteLine($"Marca con mayor margen de ganancias : {BrandWithHighestProfitMargin.BrandName}, Margen : {BrandWithHighestProfitMargin.AverageProfitMargin}");
/*foreach (var product in TopSellingProductsByLocal)
{
    Console.WriteLine($"Producto: {product.TopSellingProduct.Nombre}, Local: {product.Local.Nombre}, Costo: {CurrencyConverter.ConvertToCurrencyString(product.TopSellingProduct.CostoUnitario)}");
}*/