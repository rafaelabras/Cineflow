using System.ComponentModel.DataAnnotations;
using Cineflow.models.cinema;
using Cineflow.models.enums;

namespace Cineflow.dtos.cinema;

public class CriarSessaoDto
{
    [Required]
    public int? Id_filme { get; set; } 
    [Required]
    public int? Id_sala { get; set; }
    [Required]
    public DateTime? data_sessao { get; set; }
    [Required]
    public DateTime? horario_inicio { get; set; }
    [Required]
    public DateTime? horario_fim { get; set; }
    [Required]
    public decimal? preco_sessao { get; set; }
    public Idioma? idioma_audio { get; set; }
    public Idioma? idioma_legenda { get; set; }
    
}