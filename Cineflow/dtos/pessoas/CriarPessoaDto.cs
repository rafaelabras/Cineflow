using System.ComponentModel.DataAnnotations;

namespace Cineflow.dtos.pessoas
{
    public class CriarPessoaDto
    {
        [Key]
        [Required]
        internal string Id { get; set; } // o id é o cpf
        [Required]
        private string? name { get; set; }
        [Required]
        [EmailAddress]
        private string? email { get; set; }
        [Required]
        private string? senha { get; set; }
        [Required]
        private DateTime? data_nascimento { get; set; }
        [Required]
        [Phone]
        private string? telefone { get; set; }


    }
}
