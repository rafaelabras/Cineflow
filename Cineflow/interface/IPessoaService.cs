using Cineflow.dtos.pessoas;
using Microsoft.AspNetCore.Mvc;

namespace Cineflow.@interface
{
    public interface IPessoaService
    {
        // define os métodos que a implementação do serviço deve ter
        bool ValidarUsuario(CriarPessoaDto pessoa);


    }
}
