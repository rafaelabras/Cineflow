using Cineflow.models.enums;

namespace Cineflow.dtos.cinema;

public class ReservaFiltroDto
{
    public Guid Id { get; set; }
    public string? Id_cliente { get; set; }
    public string? Id_sessao { get; set; }
    public StatusReserva? status { get; set; } 
    public DateTime? data_reserva { get; set; } 
}