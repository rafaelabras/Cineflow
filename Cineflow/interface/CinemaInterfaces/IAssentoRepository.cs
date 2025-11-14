using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IAssentoRepository
{
    Task<IEnumerable<Assento>> GetAssentoAync(AssentoFiltroDto dto); 
    Task<bool> DeleteAssentoAsync(int id); 
    Task<int> AddAssentoAsync(CriarAssentoDto dto);
    Task<int> PutAssentoAsync(Assento assento);
}