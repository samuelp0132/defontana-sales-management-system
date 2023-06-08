using Microsoft.EntityFrameworkCore.Storage;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Infrastructure.Repositories.UnitOfWork.Interfaces;

namespace SalesManagementSystem.Infrastructure.Repositories.UnitOfWork;

public class SalesManagementUnitOfWork : ISalesManagementUnitOfWork
{
    private readonly SalesManagementContext DBContext;

    public IGenericRepository<VentaDetalle> VentaDetalleRepository { get; }
    public IGenericRepository<Producto> ProductoRepository { get; }
    public SalesManagementUnitOfWork(SalesManagementContext dBContext,
        IGenericRepository<VentaDetalle> ventaDetalleRepository, IGenericRepository<Producto> productoRepository)
    {
        DBContext = dBContext;
        VentaDetalleRepository = ventaDetalleRepository;
        ProductoRepository = productoRepository;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return DBContext.Database.BeginTransaction();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await DBContext.Database.BeginTransactionAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DBContext.Dispose();
        }
    }

    public Task<int> SaveChangesAsync()
    {
        return DBContext.SaveChangesAsync();
    }
}
