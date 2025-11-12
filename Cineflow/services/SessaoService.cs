using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;

namespace Cineflow.services;

public class SessaoService : ISessaoService
{
    public async Task<Result<IEnumerable<Sessão>>> GetSessaoAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteSessaoAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Sessão>> CriarSessaoAsync(CriarSessaoDto criarFilmeDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<CriarSessaoDto>> PutSessaoAsync(Guid id, CriarSessaoDto criarSessaoDto)
    {
        throw new NotImplementedException();
    }
}