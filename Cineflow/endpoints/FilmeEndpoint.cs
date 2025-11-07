using System.Net;
using Cineflow.dtos.cinema;
using Cineflow.extensions;
using Cineflow.@interface;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.pessoas;
using Microsoft.AspNetCore.Mvc;

namespace CineFlow.Endpoints;

public static class FilmeEndpoint
{
    public static void MapFilmeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/filme", async ([FromServices] IFilmeService filmeService, [FromBody] CriarFilmeDto dto) =>
        {
            var verificarDto = await filmeService.CriarFilmeAsync(dto);

            if (!verificarDto.IsSuccess)
            {
                return verificarDto.ToActionResult("Não foi possível criar o filme.",
                    null, HttpStatusCode.BadRequest);
            }

            return verificarDto.ToActionResult(null, "Filme criado com sucesso", HttpStatusCode.Created);
        });

        app.MapGet("/getAllFilmes", async ([FromServices] IFilmeService filmeService) =>
        {
            var filmes = await filmeService.GetFilmesAsync();

            if (!filmes.IsSuccess)
            {
                return filmes.ToActionResult("Houve um erro ao carregar os filmes.", null, HttpStatusCode.BadRequest);
            }
            
            return filmes.ToActionResult(null, "Filmes encontrados com sucesso", HttpStatusCode.OK);
        });

    }

}