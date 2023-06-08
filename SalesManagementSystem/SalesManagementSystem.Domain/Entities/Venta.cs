using SalesManagementSystem.Domain.Common;

namespace SalesManagementSystem.Domain.Entities;

public class Venta : EntidadBase
{
    public double TotalPesos { get; set; }
    public DateTimeOffset Fecha { get; set; }
    public double Neto { get; set; }
    public int Iva { get; set; }
    public Local Local { get; set; }
}