using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Assento
    {
        [Key]
        [Required]
        private int Id { get; set; }
        private int Id_sala { get; set; }
        private Sala? sala { get; set; }
        private char? fila { get; set; }
        private int? numero { get; set; }
        private bool ocupado { get; set; }

    }
}
