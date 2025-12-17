using System.Net;
using Cineflow.dtos.cinema;
using Cineflow.extensions;
using Cineflow.@interface.CinemaInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CineFlow.Endpoints;

public class AvaliacaoEndpoint
{
    public static void MapAvaliacaoEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/avaliacao",
            async ([FromServices] IAvaliacaoService avaliacaoService, [FromBody] CriarAvaliacaoDto dto) =>
            {
                var create = await avaliacaoService.AddAvaliacaoAsync(dto);

                if (!create.IsSuccess)
                    return create.ToActionResult("Não foi possível criar a avaliacao.", null,
                        HttpStatusCode.BadRequest);
                
                return create.ToActionResult(null , "Avaliacao criada com sucesso!", HttpStatusCode.Created);
            });

        app.MapGet("/avaliacao", async ([FromServices] IAvaliacaoService avaliacaoService, int? id,
            string? id_cliente,
            string? Id_filme,
            int? Nota,
            string? id_reserva,
            string? Comentario,
            DateTime? Data_avaliacao ) =>
        {
            var filtro = new AvaliacaoFiltroDto
            {
                Id = id,
                comentario = Comentario,
                Id_cliente = id_cliente,
                Id_reserva = id_reserva,
                id_filme = Id_filme,
                nota = Nota,
                data_avaliacao = Data_avaliacao
            };
            
            var query = await avaliacaoService.GetAvaliacaoAsync(filtro);
            
            if (!query.IsSuccess)
                return query.ToActionResult("Não foi possível encontrar a(s) avaliaçõe(s).", null, HttpStatusCode.BadRequest);

            return query.ToActionResult(null,  "Avaliaçõe(s) encontrada(s) com sucesso!", HttpStatusCode.OK);
        });

        app.MapDelete("/avaliacao", async([FromServices] IAvaliacaoService avaliacaoService, [FromQuery] int id) =>
        {
            var delete = await avaliacaoService.DeleteAvaliacaoAsync(id);
            
            if (!delete.IsSuccess && delete.Value == false)
                return delete.ToActionResult("Não foi possível deletar a avaliacao", null, HttpStatusCode.BadRequest);
            
            return delete.ToActionResult(null, "Avaliacao deletada com sucesso!", HttpStatusCode.NoContent);
        });

        app.MapPut("/avaliacao", async([FromServices] IAvaliacaoService avaliacaoService, [FromQuery] int id, [FromBody] CriarAvaliacaoDto dto) =>
        {
            var put =  await avaliacaoService.PutAvaliacaoAsync(id, dto);
            
            if (!put.IsSuccess)
                return put.ToActionResult("Não foi possível modificar a avaliacao.", null, HttpStatusCode.BadRequest);
            
            return put.ToActionResult(null, "Avaliacao atualizada com sucesso!", HttpStatusCode.OK);
        });


    }
    
}