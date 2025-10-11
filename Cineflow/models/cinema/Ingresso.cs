using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Ingresso
    {
        [Key]
        private Guid ID { get; set; } = new Guid();
        [Required]
        private string? Id_sessao { get; set; }
        [Required]
        private string? Id_assento { get; set; }
        [Required]
        private string? Id_reserva { get; set; }
        [Required]
        private decimal preco { get; set; }
        private bool status { get; set; } // se o ingresso foi utilizado ou não


    }
}
