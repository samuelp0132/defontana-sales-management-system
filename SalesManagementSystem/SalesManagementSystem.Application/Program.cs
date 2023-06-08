using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesManagementSystem.Application.Sales.Interfaces;
using SalesManagementSystem.Application.Sales.Services;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Infrastructure.CrossCutting;
using SalesManagementSystem.Infrastructure.ServiceExtension;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

// Register your services
services.AddDatabaseServices(configuration);
services.AddRegistration();

services.AddTransient<ISalesService, SalesService>();

// Build the service provider
var serviceProvider = services.BuildServiceProvider();
var SalesService = serviceProvider.GetRequiredService<ISalesService>();

var sales = await SalesService.GetSalesAsync();
DrawTable(sales);
//Console.WriteLine("Codigo:{0} , Id: {1}, CostoUnitario: {2}, Modelo: {3}, MarcaID: {4}, Marca Nombre: {5} ", sales.Codigo, sales.Id, sales.CostoUnitario, sales.Modelo, sales.Marca.Id, sales.Marca.Nombre);

void DrawTable(IEnumerable<VentaDetalle> data)
{
    Console.WriteLine("---------------------------------------------------------------");
    Console.WriteLine("|   ID   |   ID Venta   |  Precio Unitario  |  Cantidad  |  Total Linea  |   ID Producto  |");
    Console.WriteLine("---------------------------------------------------------------");

    foreach (var ventaDetalle in data)
    {
        Console.WriteLine($"|  {ventaDetalle.Id,-5} |  {ventaDetalle.IdVenta,-11} |  {ventaDetalle.PrecioUnitario,-16} |  {ventaDetalle.Cantidad,-10} |  {ventaDetalle.TotalLinea,-14} |  {ventaDetalle.IdProducto,-14} |");
    }

    Console.WriteLine("---------------------------------------------------------------");
}
