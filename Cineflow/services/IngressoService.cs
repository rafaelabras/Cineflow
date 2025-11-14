using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;

namespace Cineflow.services;

public class IngressoService : IIngressoService
{
    public async Task<Result<IEnumerable<Ingresso>>> GetIngressoAsync(IngressoFiltroDto filtro)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteIngressoAsync(Guid ID)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Ingresso>> CriarIngressoAsync(CriarIngressoDto ingresso)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<CriarIngressoDto>> PutIngressoAsync(Guid id, CriarIngressoDto criarIngressoDto)
    {
        throw new NotImplementedException();
    }
}