using System.Net;
using Cineflow.dtos.cinema;
using Cineflow.extensions;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Microsoft.AspNetCore.Mvc;

namespace CineFlow.Endpoints;

public static class AssentoEndpoint
{
    public static void MapAssentoEndpoins(this IEndpointRouteBuilder app)
    {
        app.MapPost("/assento", async([FromServices]IAssentoService assentoService, [FromBody]CriarAssentoDto assento) =>
        {
            var criar = await assentoService.CriarAssentoAsync(assento);
            
            if (criar.IsSuccess)
                return criar.ToActionResult(null, "Assento criado com sucesso", HttpStatusCode.Created);

            return criar.ToActionResult(null, "O assento não foi criado.", HttpStatusCode.BadRequest);
        });

        app.MapGet("/assento", async ([FromServices] IAssentoService assentoService, int? id,
            int? id_sala,
            char? Fila,
            int? Numero,
            bool? Status ) => {
            var filtro = new AssentoFiltroDto
            {   Id = id,
                Id_sala = id_sala,
                fila = Fila,
                numero = Numero,
                status = Status };
            
            var query = await assentoService.GetAssentoAsync(filtro);
            
            if (query.IsSuccess)
                return query.ToActionResult(null, "Assento(s) encontrados com sucesso", HttpStatusCode.OK);

            return query.ToActionResult("O(s) assento(s) não foram encontrados.", null, HttpStatusCode.NotFound);

        });

        app.MapPut("/assento",
            async ([FromServices] IAssentoService assentoService, [FromQuery] int id,
                [FromBody] CriarAssentoDto assento) =>
            {
            var update = await assentoService.PutAssentoAsync(id, assento);

            if (update.IsSuccess)
                return update.ToActionResult(null, "Assento atualizado com sucesso", HttpStatusCode.OK);

            return update.ToActionResult("Não foi possível atualizar o assento", null, HttpStatusCode.BadRequest);
            });
        
        
        app.MapDelete("/assento", async([FromServices]IAssentoService assentoService, [FromQuery] int id) =>
        {
            var assento = await assentoService.DeleteAssentoAsync(id);

            if (!assento.IsSuccess & assento.Value == false)
                return assento.ToActionResult("Não foi possível criar o assento", null, HttpStatusCode.BadRequest);
            
            return assento.ToActionResult(null, "Assento deletado com sucesso", HttpStatusCode.NoContent);
        });
        


    }
    
}