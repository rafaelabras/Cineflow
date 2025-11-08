using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface ISalaService
{
    public Task<Result<IEnumerable<Sala>>> GetSalaAsync();
    public Task<Result<bool>> DeleteSalaAsync(int ID);
    public Task<Result<Sala>> CriarSalaAsync(CriarSalaDto criarSalaDto);
    
    public Task<Result<Sala>> PutSalaAsync(int id,CriarSalaDto criarSalaDto);
}