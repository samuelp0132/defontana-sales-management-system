using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalesManagementSystem.Domain.Entities;
using SalesManagementSystem.Infrastructure.EntitiesSettings.Local;
using SalesManagementSystem.Infrastructure.EntitiesSettings.Marca;
using SalesManagementSystem.Infrastructure.EntitiesSettings.Producto;
using SalesManagementSystem.Infrastructure.EntitiesSettings.Venta;
using SalesManagementSystem.Infrastructure.EntitiesSettings.VentaDetalle;

namespace SalesManagementSystem.Infrastructure;

public class SalesManagementContext : DbContext 
{
    public virtual DbSet<Producto> Producto { get; set; }
    public virtual DbSet<VentaDetalle> VentaDetalle { get; set; }
    public virtual DbSet<Venta> Venta { get; set; }
    public virtual DbSet<Marca> Marca { get; set; }
    public virtual DbSet<Local> Local { get; set; }
    public SalesManagementContext(DbContextOptions<SalesManagementContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        VentaSetting.SetEntityProperty(modelBuilder.Entity<Venta>());
        VentaDetalleSetting.SetEntityProperty(modelBuilder.Entity<VentaDetalle>());
        LocalSetting.SetEntityProperty(modelBuilder.Entity<Local>());
        MarcaSetting.SetEntityProperty(modelBuilder.Entity<Marca>());
        ProductoSetting.SetEntityProperty(modelBuilder.Entity<Producto>());
        base.OnModelCreating(modelBuilder);
    }
}