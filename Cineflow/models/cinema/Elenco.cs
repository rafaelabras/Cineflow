using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Elenco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? nome { get; set; }
        [Required]
        public string? genero { get; set; }
        [Required]
        public DateTime? data_nascimento { get; set; }
        [Required]
        public string? nacionalidade { get; set; }
    }
}
