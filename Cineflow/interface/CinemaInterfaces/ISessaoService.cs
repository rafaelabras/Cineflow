using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface ISessaoService
{
    public Task<Result<IEnumerable<Sessão>>> GetSessaoAsync(SessaoFiltroDto filtro);
    public Task<Result<bool>> DeleteSessaoAsync(Guid ID);
    public Task<Result<Sessão>> CriarSessaoAsync(CriarSessaoDto criarFilmeDto);
    
    public Task<Result<CriarSessaoDto>> PutSessaoAsync(Guid id,CriarSessaoDto criarSessaoDto);
    
}