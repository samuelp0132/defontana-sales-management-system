using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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


