using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IFilmeService
{
    public Task<Result<IEnumerable<Filme>>> GetFilmesAsync();
    public Task<Result<CriarFilmeDto>> CriarFilmeAsync(CriarFilmeDto criarFilmeDto);

}