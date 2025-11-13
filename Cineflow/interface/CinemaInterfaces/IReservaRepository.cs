using Cineflow.dtos.cinema;
using Cineflow.models.cinema;

namespace Cineflow.@interface.CinemaInterfaces;

public interface IReservaRepository
{
    Task<IEnumerable<Reserva>> GetReservaAsync(ReservaFiltroDto filtro); 
    Task<bool> DeleteReservaAsync(Guid id); 
    Task<int> AddReservaAsync(Reserva dto);
    Task<int> PutReservaAsync(Reserva reserva);
}