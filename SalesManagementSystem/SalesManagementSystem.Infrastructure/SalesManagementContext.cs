using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Infrastructure;

public class SalesManagementContext : DbContext 
{
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }
    public virtual DbSet<Venta> Ventas { get; set; }
    public virtual DbSet<Marca> Marcas { get; set; }
    public virtual DbSet<Local> Locals { get; set; }
    public SalesManagementContext(DbContextOptions<SalesManagementContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
    }
}