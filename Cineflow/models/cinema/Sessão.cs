using Cineflow.models.enums;
using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Sessão
    {
        [Key]
        [Required]
         public Guid ID { get; set; }
        [Required]
        public int? Id_filme { get; set; } // mesmo utilizando o dapper de ORM é interessante manter propriedades de navegação pois filme e sala nunca seriam nulos praticamente
        public Filme? filme { get; set; }
        [Required]
        public int? Id_sala { get; set; }
        public Sala? sala { get; set; }
        [Required]
        public DateTime? data_sessao { get; set; }
        [Required]
        public DateTime? horario_inicio { get; set; }
        [Required]
        public DateTime? horario_fim { get; set; }
        [Required]
        public decimal? preco_sessao { get; set; }
        
        public decimal? receita_total { get; set; }
        public Idioma? idioma_audio { get; set; }
        public Idioma? idioma_legenda { get; set; }

        public Sessão()
        {
            
        }

    }
}
