using Cineflow.commons;
using Cineflow.dtos.pessoas;
using Cineflow.extensions;
using Cineflow.@interface;
using Cineflow.models.pessoas;
using Cineflow.services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CineFlow.Endpoints;


// Toda pessoa é criada automaticamente como um cliente
public static class PessoaEndpoints
{

    public static void MapPessoaEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapPost("/pessoa", async ([FromServices] IPessoaService _pessoaService,[FromBody] CriarClienteDto pessoa) => 
        {
            var result = await _pessoaService.AddClienteAsync(pessoa);

            if (!result.IsSuccess)
            {
                return result.ToActionResult(result.Error, null, System.Net.HttpStatusCode.BadRequest);
            }

            return result.ToActionResult(null, "Usuário criado com sucesso", System.Net.HttpStatusCode.Created);
        });

        app.MapGet("/", () => "Hello world!");

    }
}