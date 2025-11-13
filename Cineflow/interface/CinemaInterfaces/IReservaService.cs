using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IReservaService
{
    Task<Result<IEnumerable<Reserva>>> GetReservaAsync(ReservaFiltroDto dto);
    Task<Result<bool>> DeleteReservaAsync(Guid ID);
    Task<Result<Reserva>> CriarReservaAsync(CriarReservaDto criarReservaDto);
    
    Task<Result<CriarReservaDto>> PutReservaAsync(Guid id,CriarReservaDto criarReservaDto);
}