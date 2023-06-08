using SalesManagementSystem.Domain.Common;

namespace SalesManagementSystem.Domain.Entities;

public class Producto : EntidadBase
{
    public string Nombre { get; set; }
    public int Codigo { get; set; }
    public string Modelo { get; set; }
    public double Costo { get; set; }
    public Marca Marca { get; set; }
    
}