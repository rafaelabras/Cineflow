using System.Net;
using Cineflow.dtos.cinema;
using Cineflow.extensions;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Microsoft.AspNetCore.Mvc;

namespace CineFlow.Endpoints;

public static class SalaEndpoint
{
    public static void MapSalaEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/sala", async ([FromServices] ISalaService salaService, [FromBody] CriarSalaDto dto) =>
        {
            var salaCreate = await salaService.CriarSalaAsync(dto);

            if (!salaCreate.IsSuccess)
                return salaCreate.ToActionResult("Não foi possível criar a sala.", null, HttpStatusCode.BadRequest);
            
            return salaCreate.ToActionResult(null, "Sala criada com sucesso", HttpStatusCode.Created);
        });

        app.MapDelete("/sala", async ([FromServices] ISalaService salaService, [FromQuery] int salaId) =>
        {
            var salaDelete = await salaService.DeleteSalaAsync(salaId);
            if (!salaDelete.IsSuccess) 
                return salaDelete.ToActionResult("Não foi possível deletar a sala.", null, HttpStatusCode.NotFound);
            
            return salaDelete.ToActionResult(null, "Sala deletada com sucesso", HttpStatusCode.NoContent);
        });

        app.MapGet("/sala", async ([FromServices] ISalaService salaService) =>
        {
            var sala = await salaService.GetSalaAsync();
            
            if (!sala.IsSuccess)
                return sala.ToActionResult("Salas não encontradas.", null, HttpStatusCode.NotFound);
            
            return sala.ToActionResult(null, "Salas encontradas!", HttpStatusCode.OK);
        });

        app.MapGet("/salaId", async([FromServices] ISalaService salaService, [FromQuery] int salaId) =>
        {
            var sala = await salaService.GetSalaByIDAsync(salaId);
            
            if (!sala.IsSuccess)
                return sala.ToActionResult("Não foi possível encontrar a sala.", null, HttpStatusCode.NotFound);
            
            return sala.ToActionResult(null , "Sala encontrada!", HttpStatusCode.OK);
        });
        
        app.MapPut("/sala", async([FromServices] ISalaService salaServices, [FromBody] CriarSalaDto updateSala, [FromQuery] int id) =>
        {
            var putSala = await salaServices.PutSalaAsync(id, updateSala);
            
            if (!putSala.IsSuccess)
                putSala.ToActionResult("Não foi possível  atualizar a sala, verifique a existencia do ID ou os Dados.", null, HttpStatusCode.BadRequest);
            
            return putSala.ToActionResult(null , "Sala atualizada com sucesso", HttpStatusCode.NoContent);
            
        });




    }
    
}