using Cineflow.enums;
using System.ComponentModel.DataAnnotations;

namespace Cineflow.models.cinema
{
    public class Sessão
    {
        [Key]
        [Required]
       private Guid ID { get; set; } = new Guid();
        [Required]
        private int? Id_filme { get; set; } // mesmo utilizando o dapper de ORM é interessante manter propriedades de navegação pois filme e sala nunca seriam nulos praticamente
        private Filme? filme { get; set; }
        [Required]
        private int? Id_sala { get; set; }
        private Sala? sala { get; set; }
        [Required]
        private DateTime horario_inicio { get; set; }
        [Required]
        private DateTime horario_fim { get; set; } = horario_inico + duracao;
        [Required]
        private decimal preco_sessao { get; set; }
        
        private decimal receita_total { get; set; }
        private Idioma idioma_audio { get; set; }
        private Idioma idioma_legenda { get; set; }

        public Sessão()
        {
            
        }

    }
}
