using Microsoft.EntityFrameworkCore.Storage;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Infrastructure.Repositories.UnitOfWork.Interfaces;

public interface ISalesManagementUnitOfWork : IDisposable
{
    IGenericRepository<VentaDetalle> VentaDetalleRepository { get; }
    IGenericRepository<Producto> ProductoRepository { get; }
    Task<int> SaveChangesAsync();

    IDbContextTransaction BeginTransaction();

    Task<IDbContextTransaction> BeginTransactionAsync();
}
