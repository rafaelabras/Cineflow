using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Ingresso
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string? Id_sessao { get; set; }
        public Sessão? sessao { get; set; }
        [Required]
        public int? Id_assento { get; set; }
        public Assento? assento { get; set; }
        [Required]
        public int? Id_sala { get; set; } //
        public Sala? sala { get; set; } //
        [Required]
        public int? Id_filme { get; set; } //
        public Filme? filme { get; set; } // 
        [Required]
        public string? Id_reserva { get; set; }
        public Reserva? reserva { get; set; }
        [Required]
        public decimal preco { get; set; }
        [Required]
        public string? codigo_qr { get; set; }
        [Required]
        public DateTime? data_gerado { get; set; } = DateTime.Now;
        [Required]
        public DateTime? data_validacao { get; set; } = DateTime.Now;
        [Required]
        public Boolean? utilizado { get; set; } = false;


    }
}
