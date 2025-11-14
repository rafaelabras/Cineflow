using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IAvaliacaoService
{
    Task<Result<IEnumerable<Avaliação>>> GetAvaliacaoAsync(AvaliacaoFiltroDto dto); 
    Task<Result<bool>> DeleteAvaliacaoAsync(int id); 
    Task<Result<Avaliação>> AddAvaliacaoAsync(CriarAvaliacaoDto dto);
    Task<Result<CriarAvaliacaoDto>> PutAvaliacaoAsync(int id,CriarAvaliacaoDto dto);   
}