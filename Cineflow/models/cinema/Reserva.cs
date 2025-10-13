using Cineflow.models.pessoas;
using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Reserva
    {
        [Key]
        [Required]
        private Guid Id { get; set; } = new Guid();
        [Required]
        private string? Id_cliente { get; set; }
        [Required]
        private Pessoa? cliente { get; set; }
        [Required]
        private string? Id_sessao { get; set; }
        [Required]
        private Sessão? sessao { get; set; }
        [Required]
        private string? confirmada { get; set; } 
        [Required]
        private DateTime? data_reserva { get; set; } = DateTime.Now;


    }
}
