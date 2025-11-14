using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;

namespace Cineflow.repository;

public class IngressoRepository : IIngressoRepository
{
    public async Task<IEnumerable<Ingresso>> GetIngressoAsync(IngressoFiltroDto filtro)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteIngressoAsync(Guid ID)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddIngressoAsync(Ingresso ingresso)
    {
        throw new NotImplementedException();
    }

    public async Task<int> PutIngressoAsync(Ingresso sessao)
    {
        throw new NotImplementedException();
    }
}