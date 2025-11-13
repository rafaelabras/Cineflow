using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IElencoService
{
    public Task<Result<IEnumerable<Elenco>>> GetElencoAsync(ElencoFiltroDto filtro);
    public Task<Result<bool>> DeleteElencoAsync(int ID);
    public Task<Result<Elenco>> CriarElencoAsync(CriarElencoDto criarElencoDto);
    
    public Task<Result<CriarElencoDto>> PutElencoAsync(int id,CriarElencoDto criarElenco);
}