using System.ComponentModel.DataAnnotations;

namespace Cineflow.dtos.pessoas
{
    public class CriarClienteDto
    {
        [Key]
        [Required]
        internal string Id { get; set; } // o id é o cpf
        [Required]
        [Length(10, 40)]
        internal string? name { get; set; }
        [Required]
        [EmailAddress]
        internal string? email { get; set; }
        [Required]
        [Length(10, 50)]
        internal string? senha { get; set; }
        [Required]
        internal DateTime? data_nascimento { get; set; }
        [Required]
        [Phone]
        internal string? telefone { get; set; }


    }
}
