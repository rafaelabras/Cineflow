using Cineflow.models.pessoas;
using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Avaliação
    {
        [Key]
        [Required]
        private int Id { get; set; }
        [Required]
        private string? Id_cliente { get; set; }
        [Required]
        private Pessoa? cliente { get; set; }
        [Required]


        private string? Id_reserva{ get; set; }
        [Required]
        private Reserva? reserva { get; set; }
        [Required]
        private int nota { get; set; } // de 1 a 5
        private string? comentario { get; set; }
        private DateTime data_avaliacao { get; set; } = DateTime.Now;
        public Avaliação()
        {
        }
    }
}
