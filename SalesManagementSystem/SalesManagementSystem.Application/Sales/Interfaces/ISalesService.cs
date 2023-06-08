using SalesManagementSystem.Application.Sales.DTOs;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Sales.Interfaces;

public interface ISalesService
{

    Task<IEnumerable<VentaDetalle>> GetSalesAsync(CancellationToken cancellationToken = default);
}