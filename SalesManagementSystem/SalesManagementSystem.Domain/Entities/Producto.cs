using SalesManagementSystem.Domain.Common;

namespace SalesManagementSystem.Domain.Entities;

public class Producto : EntidadBase
{
    public string Nombre { get; set; }
    public string Codigo { get; set; }
    public string Modelo { get; set; }
    
    public long IdMarca { get; set; }
    public int CostoUnitario { get; set; }
    public Marca Marca { get; set; }
    public ICollection<VentaDetalle> Detalles { get; set; }
    
}