using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Ingresso
    {
        [Key]
        private Guid ID { get; set; } = new Guid();
        [Required]
        private string? Id_sessao { get; set; }
        private Sessão? sessao { get; set; }
        [Required]
        private int? Id_assento { get; set; }
        private Assento? assento { get; set; }
        [Required]
        private string? Id_reserva { get; set; }
        private Reserva? reserva { get; set; }
        [Required]
        private decimal preco { get; set; }
        [Required]
        private string? codigo_qr { get; set; }
        [Required]
        private DateTime? data_gerado { get; set; } = DateTime.Now;
        [Required]
        private DateTime? data_validacao { get; set; } = DateTime.Now;
        [Required]
        private Boolean? utilizado { get; set; } = false;


    }
}
