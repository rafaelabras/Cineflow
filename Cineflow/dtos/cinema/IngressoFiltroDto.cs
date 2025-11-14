namespace Cineflow.dtos.cinema;

public class IngressoFiltroDto
{
    public Guid ID { get; set; }
    public string? Id_sessao { get; set; }
    public int? Id_assento { get; set; }
    public string? Id_reserva { get; set; }
    public decimal preco { get; set; }
    public string? codigo_qr { get; set; }
    public DateTime? data_gerado { get; set; } 
    public DateTime? data_validacao { get; set; } 
    public Boolean? utilizado { get; set; } 
}