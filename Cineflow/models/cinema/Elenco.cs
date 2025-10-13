using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Elenco
    {
        [Key]
        [Required]
        private int Id { get; set; }
        [Required]
        private string? nome { get; set; }
        [Required]
        private string? personagem { get; set; }
        [Required]
        private string? papel { get; set; }
        [Required]
        private string? genero { get; set; }
        [Required]
        private DateTime? data_nascimento { get; set; }
        [Required]
        private string? nacionalidade { get; set; }
    }
}
