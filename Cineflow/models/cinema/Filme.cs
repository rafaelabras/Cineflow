using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Filme
    {

        [Key]
        [Required]
        private Guid ID { get; set; } = new Guid();
        [Required]
        private string? nome_filme { get; set; }
        [Required]
        private string? diretor { get; set; }
        [Required]
        private string? genero { get; set; }
        [Required]
        private DateTime duracao { get; set; }
        [Required]
        private DateTime data_lancamento { get; set; }
        [Required]
        private string? classificacao_indicativa { get; set; }
        [Required]
        private string? idioma { get; set; }
        [Required]
        private string? sinopse { get; set; }
        [Required]
        private string? elenco { get; set; }
        [Required]
        private string? produtora { get; set; }
        [Required]
        private string? pais_origem { get; set; }
        [Required]
        private decimal media_avaliacoes { get; set; }
        [Required]
        private int numero_avaliacoes { get; set; }

    }
}
