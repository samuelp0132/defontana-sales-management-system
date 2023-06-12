using SalesManagementSystem.Domain.Common;

namespace SalesManagementSystem.Domain.Entities;

public class Marca: EntidadBase
{
    public string Nombre { get; set; }
    public ICollection<Producto>? Productos { get; set; }
}