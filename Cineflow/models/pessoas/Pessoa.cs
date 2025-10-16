using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.pessoas
{
    public class Pessoa
    {
        [Key]
        [Required]
        private string? ID { get; set; }  // o id é o cpf
        [Required]
        [Length(10, 40)]
        private string? name { get; set; }
        [Required]
        [EmailAddress]
        private string? email { get; set; }
        [Required]
        private string? senhaHash { get; set; }
        [Required]
        private DateTime? data_nascimento { get; set; }
        [Required]
        [Phone]
        private string? telefone { get; set; }


    }
}
