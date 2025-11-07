using System.Net;
using Cineflow.dtos.cinema;
using Cineflow.extensions;
using Cineflow.@interface;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.enums;
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

        app.MapGet("/allFilmes", async ([FromServices] IFilmeService filmeService, string? nome_filme,
            string? genero,
            string? diretor,
            Idioma? idioma,
            string? pais_origem,
            string? produtora,
            string? classificacao_indicativa,
            int duracao,
            DateTime data_lancamento) =>
        {
            var filtro = new FilmeFiltroDto
            {
                nome_filme = nome_filme,
                genero = genero,
                diretor = diretor,
                idioma = idioma,
                pais_origem = pais_origem,
                produtora = produtora,
                classificacao_indicativa = classificacao_indicativa,
                duracao = duracao,
                data_lancamento = data_lancamento
            };
            
            var filmes = await filmeService.GetFilmesAsync(filtro);

            if (!filmes.IsSuccess)
            {
                return filmes.ToActionResult("Houve um erro ao carregar os filmes.", null, HttpStatusCode.BadRequest);
            }
            
            return filmes.ToActionResult(null, "Filmes encontrados com sucesso", HttpStatusCode.OK);
        });

        app.MapPut("/filme", async ([FromServices] IFilmeService filmeService, [FromBody] CriarFilmeDto dto) =>
        {
            var verificarDto = await filmeService.PutFilmeAsync(dto);

            if (!verificarDto.IsSuccess)
            {
                return verificarDto.ToActionResult("Não foi possível atualizar o filme.",
                    null, HttpStatusCode.BadRequest);
            }

            return verificarDto.ToActionResult(null, "Filme atualizado com sucesso", HttpStatusCode.OK);
        });

        app.MapDelete("/filme", async ([FromServices]  IFilmeService filmeService, [FromQuery] int filmeId) =>
        {
            var deletarFilme = await filmeService.DeleteFilmeAsync(filmeId);

            if (!deletarFilme.IsSuccess)
            {
                return deletarFilme.ToActionResult("Não foi possível deletar o filme.", null, HttpStatusCode.BadRequest); 
            }
            
            return deletarFilme.ToActionResult(null, "Filme deletado com sucesso!!", HttpStatusCode.NoContent);
        });


    }

}