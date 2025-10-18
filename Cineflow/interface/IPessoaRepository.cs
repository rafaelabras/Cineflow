using Cineflow.models.pessoas;

namespace Cineflow.@interface.IPessoaRepository;

 public interface IPessoaRepository
{
    Task<int> AddAsyncCliente(Cliente cliente);
}
