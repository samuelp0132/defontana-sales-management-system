using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalesManagementSystem.Infrastructure.ServiceExtension;

public static class DatabaseConfigurationService
{
    /// <summary>
    /// Adds database services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="configuration">The IConfiguration instance used to retrieve the database connection string.</param>
    public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<SalesManagementContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DB_CONNECTION_STRING"))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine, LogLevel.Information)
        );
    }
}