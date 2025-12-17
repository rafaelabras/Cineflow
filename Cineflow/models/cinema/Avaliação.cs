using Cineflow.models.pessoas;
using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Avaliação
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Id_cliente { get; set; }
        [Required]
        public Cliente? cliente { get; set; }
        [Required]
        public string? Id_reserva{ get; set; }
        [Required]
        public Reserva? reserva { get; set; }
        [Required]
        public string? id_filme { get; set; }
        [Required]
        public Filme? filme { get; set; }
        [Required]
        public int nota { get; set; } // de 1 a 5
        public string? comentario { get; set; }
        public DateTime data_avaliacao { get; set; }
        public Avaliação()
        {
        }
    }
}
