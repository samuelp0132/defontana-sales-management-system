using SalesManagementSystem.Domain.Common;

namespace SalesManagementSystem.Domain.Entities;

public class VentaDetalle : EntidadBase
{
    public double PrecioUnitario { get; set; }
    public int Cantidad { get; set; }
    public double TotalLinea { get; set; }
    public Venta Venta { get; set; }
    public Producto Producto { get; set; }
}