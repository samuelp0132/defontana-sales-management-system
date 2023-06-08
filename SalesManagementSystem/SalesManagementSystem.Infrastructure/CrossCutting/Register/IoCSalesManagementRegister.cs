using Microsoft.Extensions.DependencyInjection;
using SalesManagementSystem.Infrastructure.Repositories;
using SalesManagementSystem.Infrastructure.Repositories.UnitOfWork;
using SalesManagementSystem.Infrastructure.Repositories.UnitOfWork.Interfaces;

namespace SalesManagementSystem.Infrastructure.CrossCutting.Register;

public static class IocSalesManagementRegister
{
    public static void AddRegistration(this IServiceCollection services)
    {
        AddRegisterRepositories(services);
    }
    private static void AddRegisterRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<ISalesManagementUnitOfWork, SalesManagementUnitOfWork>();
    }
    
}