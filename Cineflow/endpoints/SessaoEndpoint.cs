using System.Net;
using Cineflow.dtos.cinema;
using Cineflow.extensions;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel;

namespace CineFlow.Endpoints;

public static class SessaoEndpoint
{
    public static void MapSessaoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/sessao", async ([FromServices] ISessaoService sessaoService, [FromBody] CriarSessaoDto dto) =>
        {
            var resultado = await sessaoService.CriarSessaoAsync(dto);

            if (!resultado.IsSuccess)
                return resultado.ToActionResult("Não foi possível criar a sessão", null, HttpStatusCode.BadRequest);

            return resultado.ToActionResult(null, "A sessão foi criada com sucesso!", HttpStatusCode.Created);
        });

        app.MapGet("/sessao", async ([FromServices] ISessaoService sessaoService, Guid ID,
            int? id_filme,
            int? id_sala,
            DateTime? data_sessao_,
            DateTime? horario_inicio_,
            DateTime? horario_fim_,
            decimal? preco_sessao_,
            Idioma? idioma_audio_,
            Idioma? idioma_legenda_) =>
        {
            var filtro = new SessaoFiltroDto
            {
                Id_filme = id_filme,
                Id_sala = id_sala,
                idioma_audio = idioma_audio_,
                data_sessao = data_sessao_,
                preco_sessao = preco_sessao_,
                idioma_legenda = idioma_legenda_,
                horario_fim = horario_fim_,
                horario_inicio = horario_inicio_
            };

            var query = await sessaoService.GetSessaoAsync(filtro);

            if (!query.IsSuccess)
                return query.ToActionResult("Não foi possível encontrar a(s) sala(s).", null, HttpStatusCode.BadRequest);
            
            return query.ToActionResult(null, "Sala(s) encontradas com sucesso!", HttpStatusCode.OK);
        });

        app.MapPut("/sessao", async([FromServices] ISessaoService sessaoService, [FromBody] CriarSessaoDto dto, [FromQuery] Guid id) =>
        {
            var put =  await sessaoService.PutSessaoAsync(id, dto);
            if (!put.IsSuccess)
                return put.ToActionResult("Não foi possível atualizar a sala.", null, HttpStatusCode.BadRequest);
            
            return put.ToActionResult(null, "Sala atualizada", HttpStatusCode.OK);
        });

        app.MapDelete("/sessao", async([FromServices] ISessaoService sessaoService, [FromQuery] Guid id) =>
        {
            var delete =  await sessaoService.DeleteSessaoAsync(id);
           
            if (!delete.IsSuccess && delete.Value == false)
                delete.ToActionResult("Não foi possível excluir a sessão.", null, HttpStatusCode.BadRequest);

            return delete.ToActionResult(null, "A sessão foi excluida com sucesso!", HttpStatusCode.NoContent);
        });
        
    }
    
}