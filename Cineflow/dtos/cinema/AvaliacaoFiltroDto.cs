namespace Cineflow.dtos.cinema;

public class AvaliacaoFiltroDto
{
    public int Id { get; set; }
    public string? Id_cliente { get; set; }
    public string? Id_reserva{ get; set; }
    public string? id_filme { get; set; }
    public int nota { get; set; } // de 1 a 5
    public string? comentario { get; set; }
    public DateTime data_avaliacao { get; set; }
}