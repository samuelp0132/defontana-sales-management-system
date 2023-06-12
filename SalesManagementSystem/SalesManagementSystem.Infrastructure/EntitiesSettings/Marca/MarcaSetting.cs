using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagementSystem.Infrastructure.EntitiesSettings.Marca;

public class MarcaSetting
{
    protected MarcaSetting()
    {
        
    }
    public static void SetEntityProperty(EntityTypeBuilder<Domain.Entities.Marca> entity)
    {
        entity.ToTable("Marca");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("ID_Marca");
        entity.Property(e => e.Nombre).HasColumnName("Nombre");
        entity.HasMany(m => m.Productos)
            .WithOne(p => p.Marca)
            .HasForeignKey(p => p.IdMarca);
        entity.UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}