using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }
        public string? tipo_sala { get; set; }
        public int capacidade { get; set; }
        public int assentos_ocupados { get; set; }

    }
}
