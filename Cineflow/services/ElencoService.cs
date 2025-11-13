using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;

namespace Cineflow.services;

public class ElencoService : IElencoService
{
    public async Task<Result<IEnumerable<Elenco>>> GetElencoAsync(ElencoFiltroDto filtro)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteElencoAsync(int ID)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Elenco>> CriarElencoAsync(CriarElencoDto criarElencoDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<CriarElencoDto>> PutElencoAsync(int id, CriarElencoDto criarElenco)
    {
        throw new NotImplementedException();
    }
}