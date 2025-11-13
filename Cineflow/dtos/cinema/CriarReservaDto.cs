using Cineflow.models.cinema;
using Cineflow.models.enums;
using Cineflow.models.pessoas;

namespace Cineflow.dtos.cinema;

public class CriarReservaDto
{
    public string? Id_cliente { get; set; }
    public Cliente? cliente { get; set; }
    public string? Id_sessao { get; set; }
    public Sessão? sessao { get; set; }
    public StatusReserva? status { get; set; } 
    public DateTime? data_reserva { get; set; } = DateTime.Now;
}