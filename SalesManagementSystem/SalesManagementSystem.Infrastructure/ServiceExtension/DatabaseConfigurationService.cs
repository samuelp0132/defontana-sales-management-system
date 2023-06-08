using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SalesManagementSystem.Infrastructure.ServiceExtension;

public static class DatabaseConfigurationService
{
    public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<SalesManagementContext>(options =>
            options.UseSqlServer(
                    configuration.GetConnectionString("DB_CONNECTION_STRING"))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());
    }
}