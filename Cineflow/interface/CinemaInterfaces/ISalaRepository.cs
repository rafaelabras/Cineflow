using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface ISalaRepository
{
    public Task<int> CreateSalaAsync(CriarSalaDto criarSalaDto);
    public Task<int> DeleteSalaAsync(int id);
    public Task<IEnumerable<Sala>> GetSalasAsync();
    public Task<int> PutSalaAsync (Sala sala);
}