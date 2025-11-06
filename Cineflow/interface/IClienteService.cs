using System.Net;
using Cineflow.commons;
using Cineflow.dtos.pessoas;
using Cineflow.helpers;
using Microsoft.AspNetCore.Mvc;

namespace Cineflow.@interface
{
    public interface IClienteService
    {
        // define os métodos que a implementação do serviço deve ter
        Task<Result<RetornarClienteDto>> AddClienteAsync(CriarClienteDto pessoa);
        Task<Result<IEnumerable<RetornarClienteDto>>> ReturnAllClientesAsync();
        
        Task<Result<HttpStatusCode>> DeleteClienteAsync(string id);
        
        Task<Result<RetornarClienteDto>> PutClienteAsync(Guid ID,CriarClienteDto pessoa);
        
    }
}
