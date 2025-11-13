using System.Net;
using Cineflow.dtos.cinema;
using Cineflow.extensions;
using Cineflow.@interface.CinemaInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CineFlow.Endpoints;

public static class ElencoEndpoint
{
    public static void MapElencoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/elenco", async ([FromServices] IElencoService elencoService, [FromBody] CriarElencoDto dto) =>
        {
            var elencoPost = await elencoService.CriarElencoAsync(dto);
            
            if (!elencoPost.IsSuccess)
                return elencoPost.ToActionResult("Não foi possível criar o elenco.", null, HttpStatusCode.BadRequest);
            
            return elencoPost.ToActionResult(null, "Elenco Criado.", HttpStatusCode.Created);
        });

        app.MapGet("/elenco", async ([FromServices] IElencoService elencoService,
            int id,
            string? Nome,
            string? Genero,
            DateTime? Data_nascimento,
            string? Nacionalidade) =>
        {
            var elencoFiltro = new ElencoFiltroDto()
            {
                Id = id,
                nome = Nome,
                genero = Genero,
                data_nascimento = Data_nascimento,
                nacionalidade = Nacionalidade
            };
            
            var query = await elencoService.GetElencoAsync(elencoFiltro);
            
            if (!query.IsSuccess)
                    query.ToActionResult("Não foi possível encontrar o elenco", null, HttpStatusCode.BadRequest);
            
            return query.ToActionResult(null, "Elenco encontrado.", HttpStatusCode.OK);
        });

        app.MapDelete("/elenco", async ([FromServices] IElencoService elencoService, [FromQuery] int id) =>
            {
                var elencoDelete = await elencoService.DeleteElencoAsync(id);
                
                if (!elencoDelete.IsSuccess && elencoDelete.Value ==  false)
                    return elencoDelete.ToActionResult("Não foi possível excluir o elenco", null, HttpStatusCode.BadRequest);

                return elencoDelete.ToActionResult(null, "Elenco deletado com sucesso", HttpStatusCode.NoContent);
            });

        app.MapPut("/elenco", async([FromServices] IElencoService elencoService, [FromQuery] int id, [FromBody] CriarElencoDto dto) =>
        {
            var elencoPut = await elencoService.PutElencoAsync(id, dto);

            if (!elencoPut.IsSuccess)
                return elencoPut.ToActionResult("Não foi possível atualizar o elenco", null, HttpStatusCode.BadRequest);
            
            return elencoPut.ToActionResult(null, "Elenco atualizado com sucesso", HttpStatusCode.OK);
        });
        
    }
    
}