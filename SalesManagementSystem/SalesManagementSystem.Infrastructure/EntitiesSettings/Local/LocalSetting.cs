using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesManagementSystem.Infrastructure.EntitiesSettings.Local;

public class LocalSetting
{
    public static void SetEntityProperty(EntityTypeBuilder<Domain.Entities.Local> entity)
    {
        entity.ToTable("Local");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("ID_Local");
        entity.Property(e => e.Nombre).HasColumnName("Nombre");
        entity.Property(e => e.Direccion).HasColumnName("Direccion");
        entity.UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}