using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.IClienteRepository;

public interface IFilmeRepository
{
    public Task<IEnumerable<Filme>> GetFilmesAsync();
    public Task<IEnumerable<Filme>> GetFilmesByIDAsync(int ID);
    public Task<bool> DeleteFilmeAsync(int ID);
    public Task<int> AddFilmeAsync(CriarFilmeDto filme);
    
    public Task<int>  PutFilmeAsync(CriarFilmeDto filme);
    
}