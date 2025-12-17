namespace Cineflow.dtos.cinema;

public class CriarIngressoDto
{
    
    public string? Id_sessao { get; set; }
    public int? Id_assento { get; set; }
    public string? Id_reserva { get; set; }
    public int? Id_sala { get; set; } //
    public int? Id_filme { get; set; } //
    public decimal preco { get; set; }
    public string? codigo_qr { get; set; }
    public DateTime? data_gerado { get; set; } = DateTime.Now;
    public DateTime? data_validacao { get; set; } = DateTime.Now;
    public Boolean? utilizado { get; set; } = false;
    
}