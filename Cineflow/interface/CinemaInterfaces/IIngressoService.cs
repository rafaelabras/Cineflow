using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IIngressoService
{
    public Task<Result<IEnumerable<Ingresso>>> GetIngressoAsync(IngressoFiltroDto filtro);
    public Task<Result<bool>> DeleteIngressoAsync(Guid ID);
    public Task<Result<Ingresso>> CriarIngressoAsync(CriarIngressoDto ingresso);
    
    public Task<Result<CriarIngressoDto>> PutIngressoAsync(Guid id,CriarIngressoDto criarIngressoDto);   
}