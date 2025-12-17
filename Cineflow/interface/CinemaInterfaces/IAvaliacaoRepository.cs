using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IAvaliacaoRepository
{
    Task<IEnumerable<Avaliação>> GetAvaliacaoAync(AvaliacaoFiltroDto dto); 
    Task<bool> DeleteAvaliacaoAsync(int id); 
    Task<int> AddAvaliacaoAsync(CriarAvaliacaoDto dto);
    Task<int> PutAvaliacaoAsync(Avaliação avaliacao);
}