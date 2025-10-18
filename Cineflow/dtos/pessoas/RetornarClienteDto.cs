using System.ComponentModel.DataAnnotations;

namespace Cineflow.dtos.pessoas
{
    public class RetornarClienteDto
    {

        [Key]
        [Required]
        public Guid? ID { get; set; }
        [Required]
        [Length(10, 40)]
        public string? name { get; set; }
        [Required]
        public string? genero { get; set; }
        [Required]
        [EmailAddress]
        public string? email { get; set; }
        [Required]
        public DateTime? data_nascimento { get; set; }
        [Required]
        [Phone]
        public string? telefone { get; set; }


    }
}
