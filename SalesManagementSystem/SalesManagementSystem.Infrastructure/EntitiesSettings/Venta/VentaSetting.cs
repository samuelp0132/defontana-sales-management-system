using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagementSystem.Infrastructure.EntitiesSettings.Venta;

public class VentaSetting
{
    protected VentaSetting()
    {
        
    }
    public static void SetEntityProperty(EntityTypeBuilder<Domain.Entities.Venta> entity)
    {
        entity.ToTable("Venta");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("ID_Venta");
        entity.Property(e => e.Total).HasColumnName("Total");
        entity.Property(e => e.Fecha).HasColumnName("Fecha");
        entity.Property(e => e.IdLocal).HasColumnName("ID_Local");
        entity
            .HasOne(v => v.Local)
            .WithMany(l => l.Ventas)
            .HasForeignKey(v => v.IdLocal);
        entity.UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}