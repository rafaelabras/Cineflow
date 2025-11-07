using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.IClienteRepository;

public interface IFilmeRepository
{
    public Task<IEnumerable<Filme>> GetFilmesAsync();
    
    public Task<int> AddFilmeAsync(CriarFilmeDto filme);
    
}