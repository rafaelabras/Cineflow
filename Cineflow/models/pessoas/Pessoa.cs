using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.pessoas
{
    public class Pessoa
    {
        [Key]
        [Required]
        private string Id { get; set; } // o id é o cpf
        [Required]
        private string name { get; set; }
        [Required]
        [EmailAddress]
        private string email { get; set; }
        [Required]
        private string senhaHash { get; set; }
        [Required]
        private DateTime data_nascimento { get; set; }
        private DateTime idade { get; set; }
        [Required]
        private string telefone { get; set; }


    }
}
