using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IIngressoRepository
{
    public Task<IEnumerable<Ingresso>> GetIngressoAsync(IngressoFiltroDto filtro);
    public Task<bool> DeleteIngressoAsync(Guid ID);
    public Task<int> AddIngressoAsync(Ingresso ingresso);
    
    public Task<int> PutIngressoAsync(Ingresso sessao);
}