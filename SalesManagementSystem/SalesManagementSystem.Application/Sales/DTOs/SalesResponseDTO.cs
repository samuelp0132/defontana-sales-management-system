using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Sales.DTOs;

public class SalesResponseDTO
{
    public Int64 Id { get; set; }
    public string Nombre { get; set; }
    public string Codigo { get; set; }
    public int IdMarca { get; set; }
    public string Modelo { get; set; }
    public int CostoUnitario { get; set; }

    public Marca Marca { get; set; }
}