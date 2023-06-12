using SalesManagementSystem.Application.Sales.Interfaces;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Infrastructure.Repositories.UnitOfWork.Interfaces;

namespace SalesManagementSystem.Application.Sales.Services;

/// <summary>
/// Service class that provides operations related to sales.
/// </summary>
public class SalesService : ISalesService
{
    private readonly ISalesManagementUnitOfWork _unitOfWork;
    /// <summary>
    /// Initializes a new instance of the SalesService class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work instance to be used by the service.</param>
    public SalesService(ISalesManagementUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    /// <summary>
    /// Retrieves the sales details within a specified date range.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A collection of VentaDetalle objects representing the sales details.</returns>
    public async Task<IEnumerable<VentaDetalle>> GetSalesAsync(CancellationToken cancellationToken = default)
    {
        // Set date range
        DateTimeOffset startDate = DateTimeOffset.Now.AddDays(-30);
        DateTimeOffset endDate = DateTimeOffset.Now;
        
        return await _unitOfWork.VentaDetalleRepository.GetAll(
            orderBy: q => q.OrderByDescending(c => c.Id),
            filter: v => v.Venta.Fecha >= startDate && v.Venta.Fecha <= endDate,
            includeProperties: $"{nameof(Producto)}.{nameof(Marca)},{nameof(Producto)},{nameof(Venta)},{nameof(Venta)}.{nameof(Local)}"
            );
    }
}