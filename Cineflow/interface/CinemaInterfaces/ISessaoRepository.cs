using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface ISessaoRepository
{
    public Task<IEnumerable<Sessão>> GetSessaoAsync(SessaoFiltroDto filtro);
    public Task<bool> DeleteSessaoAsync(Guid ID);
    public Task<int> AddSessaoAsync(Sessão sessao);
    
    public Task<int> PutSessaoAsync(Sessão sessao);
}