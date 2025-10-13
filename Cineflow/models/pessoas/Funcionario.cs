namespace Cineflow.models.pessoas
{
    public class Funcionario : Pessoa
    {
        private string cargo { get; set; }
        private DateTime data_admissao { get; set; }
        private DateTime data_demissao { get; set; }
        private decimal salario { get; set; }
    }
}
