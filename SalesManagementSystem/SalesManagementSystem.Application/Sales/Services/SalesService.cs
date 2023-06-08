using SalesManagementSystem.Application.Sales.DTOs;
using SalesManagementSystem.Application.Sales.Interfaces;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Infrastructure.Repositories.UnitOfWork.Interfaces;

namespace SalesManagementSystem.Application.Sales.Services;

public class SalesService : ISalesService
{
    private readonly ISalesManagementUnitOfWork _unitOfWork;
    public SalesService(ISalesManagementUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<VentaDetalle>> GetSalesAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.VentaDetalleRepository.GetAll(includeProperties: $"{nameof(Producto)}.{nameof(Marca)},{nameof(Producto)},{nameof(Venta)},{nameof(Venta)}.{nameof(Local)}");
    }
}