using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IFilmeService
{
    public Task<Result<IEnumerable<Filme>>> GetFilmesAsync(FilmeFiltroDto filtro);
    public Task<Result<bool>> DeleteFilmeAsync(int ID);
    public Task<Result<CriarFilmeDto>> CriarFilmeAsync(CriarFilmeDto criarFilmeDto);
    
    public Task<Result<CriarFilmeDto>> PutFilmeAsync(CriarFilmeDto criarFilmeDto);
    

}