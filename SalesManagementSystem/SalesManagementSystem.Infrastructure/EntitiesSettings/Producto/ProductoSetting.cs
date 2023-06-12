using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagementSystem.Infrastructure.EntitiesSettings.Producto;

public class ProductoSetting
{
    protected ProductoSetting()
    {
        
    }
    public static void SetEntityProperty(EntityTypeBuilder<Domain.Entities.Producto> entity)
    {
        entity.ToTable("Producto");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("ID_Producto");
        entity.Property(e => e.Codigo).HasColumnName("Codigo");
        entity.Property(e => e.Modelo).HasColumnName("Modelo");
        entity.Property(e => e.CostoUnitario).HasColumnName("Costo_Unitario");
        entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");
        entity
            .HasOne(p => p.Marca)
            .WithMany(m => m.Productos)
            .HasForeignKey(p => p.IdMarca);
        entity.UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}