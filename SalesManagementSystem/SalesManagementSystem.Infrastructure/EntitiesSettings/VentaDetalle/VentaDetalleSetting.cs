using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagementSystem.Infrastructure.EntitiesSettings.VentaDetalle;

public class VentaDetalleSetting
{
    protected VentaDetalleSetting()
    {
        
    }
    public static void SetEntityProperty(EntityTypeBuilder<Domain.Entities.VentaDetalle> entity)
    {
        entity.ToTable("VentaDetalle");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("ID_VentaDetalle");
        entity.Property(e => e.IdVenta).HasColumnName("ID_Venta");
        entity.Property(e => e.PrecioUnitario).HasColumnName("Precio_Unitario");
        entity.Property(e => e.Cantidad).HasColumnName("Cantidad");
        entity.Property(e => e.TotalLinea).HasColumnName("TotalLinea");
        entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
        entity
            .HasOne(vd => vd.Venta)
            .WithMany(v => v.Detalles)
            .HasForeignKey(vd => vd.IdVenta);
        
        entity
            .HasOne(vd => vd.Producto)
            .WithMany(p => p.Detalles)
            .HasForeignKey(vd => vd.IdProducto);
        entity.UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}