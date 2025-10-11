using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Sala
    {
        [Key]
        private int Id { get; set; }
        private string? tipo_sala { get; set; }
        private int capacidade { get; set; }
        private int assentos_ocupados { get; set; }
        private decimal receita_total { get; set; }

    }
}
