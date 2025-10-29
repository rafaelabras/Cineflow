using System.ComponentModel.DataAnnotations;

namespace Cineflow.dtos.pessoas
{
    public class CriarClienteDto
    {

 
        [Required]
        public string CPF { get; set; } 
        [Required]
        [Length(10, 40)]
        public string? nome { get; set; }
        [Required]
        [EmailAddress]
        public string? email { get; set; }
        [Required]
        public string? genero { get; set; }
        [Required]
        [Length(10, 50)]
        public string? senha { get; set; }
        [Required]
        public DateTime? data_nascimento { get; set; }
        [Required]
        [Phone]
        public string? telefone { get; set; }


    }
}
