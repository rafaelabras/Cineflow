using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Assento
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int Id_sala { get; set; }
        public Sala? sala { get; set; }
        public char? fila { get; set; }
        public int? numero { get; set; }
        public bool status { get; set; }

    }
}
