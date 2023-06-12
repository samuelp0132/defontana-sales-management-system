using SalesManagementSystem.Domain.Common;

namespace SalesManagementSystem.Domain.Entities;

public class Local : EntidadBase
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public ICollection<Venta>? Ventas { get; set; }
}