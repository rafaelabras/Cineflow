
using Cineflow.dtos.pessoas;
using Cineflow.models.pessoas;

namespace Cineflow.@interface.IClienteRepository;

 public interface IClienteRepository
{
    Task<int> AddAsyncCliente(Cliente cliente);
    Task<IEnumerable<RetornarClienteDto>> ReturnAsyncAllClientes();
    Task<bool> RemoveCliente(string ID);
    
    Task<bool> PutAsyncCliente(Cliente cliente);
}
