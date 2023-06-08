using Microsoft.Extensions.DependencyInjection;
using SalesManagementSystem.Infrastructure.CrossCutting.Register;

namespace SalesManagementSystem.Infrastructure.CrossCutting;

public static class IoCRegister
{
    public static void AddRegistration(this IServiceCollection services)
    {
        IocSalesManagementRegister.AddRegistration(services);
    }
}
