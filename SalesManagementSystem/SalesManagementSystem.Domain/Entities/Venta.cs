using SalesManagementSystem.Domain.Common;

namespace SalesManagementSystem.Domain.Entities;

public class Venta : EntidadBase
{
    public int Total { get; set; }
    public DateTime Fecha { get; set; }
    public long IdLocal { get; set; }

    public Local Local { get; set; }
    public ICollection<VentaDetalle> Detalles { get; set; }
}