using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IElencoRepository
{
    public Task<IEnumerable<Elenco>> GetElencoAsync(ElencoFiltroDto filtro);
    public Task<bool> DeleteElencoAsync(int id);
    public Task<int> AddElencoAsync(CriarElencoDto dto);
    
    public Task<int> PutElencoAsync(Elenco elenco);
    
}