using System.ComponentModel.DataAnnotations;
using Cineflow.models.enums;

namespace Cineflow.dtos.cinema;

public class FilmeFiltroDto
{
    public int id { get; set; }
    public string? nome_filme { get; set; }
    public string? diretor { get; set; }
    public string? genero { get; set; }
    public int duracao { get; set; }
    public DateTime data_lancamento { get; set; }
    public string? classificacao_indicativa { get; set; }
    public Idioma? idioma { get; set; }
    public string? sinopse { get; set; }
    public string? produtora { get; set; }
    public string? pais_origem { get; set; }
    public decimal media_avaliacoes { get; set; }
    public int numero_avaliacoes { get; set; }
}