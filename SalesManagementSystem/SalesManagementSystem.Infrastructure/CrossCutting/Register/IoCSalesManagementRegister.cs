using Microsoft.Extensions.DependencyInjection;
using SalesManagementSystem.Infrastructure.Repositories;
using SalesManagementSystem.Infrastructure.Repositories.UnitOfWork;
using SalesManagementSystem.Infrastructure.Repositories.UnitOfWork.Interfaces;

namespace SalesManagementSystem.Infrastructure.CrossCutting.Register;

/// <summary>
/// Static class responsible for registering dependencies using the IServiceCollection.
/// </summary>
public static class IocSalesManagementRegister
{
    /// <summary>
    /// Extension method for IServiceCollection that adds registrations for the Sales Management System.
    /// It registers repositories and other dependencies required for the system.
    /// </summary>
    /// <param name="services">The IServiceCollection instance to add registrations to.</param>
    public static void AddRegistration(this IServiceCollection services)
    {
        AddRegisterRepositories(services);
    }
    
    /// <summary>
    /// Adds registrations for repositories and the unit of work.
    /// </summary>
    /// <param name="services">The IServiceCollection instance to add registrations to.</param>
    private static void AddRegisterRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<ISalesManagementUnitOfWork, SalesManagementUnitOfWork>();
    }
    
}