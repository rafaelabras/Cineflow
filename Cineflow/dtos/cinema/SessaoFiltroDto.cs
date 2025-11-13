using Cineflow.models.enums;

namespace Cineflow.dtos.cinema;

public class SessaoFiltroDto
{
    public Guid ID { get; set; }
    public int? Id_filme { get; set; } 
    public int? Id_sala { get; set; }
    public DateTime? data_sessao { get; set; }
    public DateTime? horario_inicio { get; set; }
    public DateTime? horario_fim { get; set; }
    public decimal? preco_sessao { get; set; }
    public Idioma? idioma_audio { get; set; }
    public Idioma? idioma_legenda { get; set; }
}