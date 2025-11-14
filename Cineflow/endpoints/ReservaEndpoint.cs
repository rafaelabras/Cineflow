using System.Net;
using Cineflow.dtos.cinema;
using Cineflow.extensions;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.enums;
using Microsoft.AspNetCore.Mvc;

namespace CineFlow.Endpoints;

public static class ReservaEndpoint
{
    public static void MapReservaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/reserva", async ([FromServices] IReservaService reservaService, [FromBody] CriarReservaDto dto) =>
        {
            var postDb = await reservaService.CriarReservaAsync(dto);
            
            if (!postDb.IsSuccess)
                return postDb.ToActionResult("Não foi possível criar a reserva", null, HttpStatusCode.InternalServerError);

            return postDb.ToActionResult(null, "Reserva criada com sucesso", HttpStatusCode.Created);
        });
        
        
        
        app.MapGet("/reserva", async ([FromServices] IReservaService reservaService, Guid id,
            string? id_cliente,
            string? id_sessao,
            StatusReserva? Status,
            DateTime? Data_reserva) =>
        {
            var filtro = new ReservaFiltroDto
            {
                Id = id,
                Id_cliente = id_cliente,
                Id_sessao = id_sessao,
                status = Status,
                data_reserva = Data_reserva
            };
            
            var query = await reservaService.GetReservaAsync(filtro);

            if (!query.IsSuccess)
                return query.ToActionResult("Não foi possível encontrar a reserva", null, HttpStatusCode.NotFound);

            return query.ToActionResult(null, "Reservas Encontradas!", HttpStatusCode.OK);
        });

        app.MapPut("/reserva", async([FromServices] IReservaService reservaService, [FromQuery] Guid id, [FromBody] CriarReservaDto dto) =>
        {
            var putDb = await reservaService.PutReservaAsync(id, dto);

            if (!putDb.IsSuccess)
                return putDb.ToActionResult("Não foi possível atualizar a reserva", null, HttpStatusCode.BadRequest);
            
            return putDb.ToActionResult(null, "Reserva atualizada com sucesso", HttpStatusCode.OK);
        });

        app.MapDelete("reserva", async ([FromServices] IReservaService IReservaService, [FromQuery] Guid id) =>
        {
            var deleteDb = await IReservaService.DeleteReservaAsync(id);
            
            if (!deleteDb.IsSuccess && deleteDb.Value == false)
                return deleteDb.ToActionResult("Não foi possível deletar a reserva", null, HttpStatusCode.NotFound);
                   
            return deleteDb.ToActionResult(null, "Reserva deletada com sucesso", HttpStatusCode.NoContent);
        });

    }
    
    
}