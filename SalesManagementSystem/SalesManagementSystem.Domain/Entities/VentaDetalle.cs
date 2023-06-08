using SalesManagementSystem.Domain.Common;

namespace SalesManagementSystem.Domain.Entities;

public class VentaDetalle : EntidadBase
{
    public long IdVenta { get; set; }
    public int PrecioUnitario { get; set; }
    public int Cantidad { get; set; }
    public int TotalLinea { get; set; }
    public long IdProducto { get; set; }

    public Venta Venta { get; set; }
    public Producto Producto { get; set; }
}