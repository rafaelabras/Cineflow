using System.ComponentModel.DataAnnotations;

namespace Cineflow.dtos.cinema;

public class CriarElencoDto
{
    [Required]
    public string? nome { get; set; }
    [Required]
    public string? personagem { get; set; }
    [Required]
    public string? papel { get; set; }
    [Required]
    public string? genero { get; set; }
    [Required]
    public DateTime? data_nascimento { get; set; }
    [Required]
    public string? nacionalidade { get; set; }
    
}