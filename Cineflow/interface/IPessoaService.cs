using Cineflow.commons;
using Cineflow.dtos.pessoas;
using Cineflow.helpers;
using Microsoft.AspNetCore.Mvc;

namespace Cineflow.@interface
{
    public interface IPessoaService
    {
        // define os métodos que a implementação do serviço deve ter
        Task<Result<RetornarClienteDto>> AddClienteAsync(CriarClienteDto pessoa);


    }
}
