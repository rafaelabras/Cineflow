using Cineflow.models.pessoas;
using System.ComponentModel.DataAnnotations;
using Cineflow.models.enums;

namespace Cineflow.models.cinema
{
    public class Reserva
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Id_cliente { get; set; }
        [Required]
        public Cliente? cliente { get; set; }
        [Required]
        public string? Id_sessao { get; set; }
        [Required]
        public Sessão? sessao { get; set; }
        [Required]
        public StatusReserva? status { get; set; } 
        [Required]
        public DateTime? data_reserva { get; set; } = DateTime.Now;


    }
}
