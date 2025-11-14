using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IAssentoService
{
    Task<Result<IEnumerable<Assento>>> GetAssentoAsync(AssentoFiltroDto dto);
    Task<Result<bool>> DeleteAssentoAsync(int ID);
    Task<Result<Assento>> CriarAssentoAsync(CriarAssentoDto dto);
    Task<Result<CriarAssentoDto>> PutAssentoAsync(int id,CriarAssentoDto criarReservaDto);
    
}