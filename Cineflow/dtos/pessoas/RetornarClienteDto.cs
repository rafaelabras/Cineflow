using System.ComponentModel.DataAnnotations;

namespace Cineflow.dtos.pessoas
{
    public class RetornarClienteDto
    {

        [Key]
        [Required]
        internal Guid? ID { get; set; }
        [Required]
        [Length(10, 40)]
        internal string? name { get; set; }
        [Required]
        [EmailAddress]
        internal string? email { get; set; }
        [Required]
        internal DateTime? data_nascimento { get; set; }
        [Required]
        [Phone]
        internal string? telefone { get; set; }


    }
}
