using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Filme
    {

        [Key]
        [Required]
        public int ID { get; set; } 
        [Required]
        public string? nome_filme { get; set; }
        [Required]
        public string? diretor { get; set; }
        [Required]
        public string? genero { get; set; }
        [Required]
        public int duracao { get; set; }
        [Required]
        public DateTime data_lancamento { get; set; }
        [Required]
        public string? classificacao_indicativa { get; set; }
        [Required]
        public string? idioma { get; set; }
        [Required]
        public string? sinopse { get; set; }
        [Required]
        public string? produtora { get; set; }
        [Required]
        public string? pais_origem { get; set; }
        [Required]
        public decimal media_avaliacoes { get; set; }
        [Required]
        public int numero_avaliacoes { get; set; }

    }
}
