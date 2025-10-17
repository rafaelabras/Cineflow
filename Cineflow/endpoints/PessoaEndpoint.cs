using Cineflow.commons;
using Cineflow.dtos.pessoas;
using Cineflow.extensions;
using Cineflow.@interface;
using Cineflow.models.pessoas;
using Cineflow.services;
using System.Runtime.CompilerServices;

namespace CineFlow.Endpoints;


// Toda pessoa é criada automaticamente como um cliente
public static class PessoaEndpoints
{

    public static void MapPessoaEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapPost("/pessoa", async (IPessoaService _pessoaService,CriarClienteDto pessoa) => 
        {
            var result = _pessoaService.ValidarUsuario(pessoa);

            if (!result.IsSuccess)
            {
                return result.ToActionResult(result.Error, null, System.Net.HttpStatusCode.BadRequest);
            }

            return result.ToActionResult(null, "Usuário criado com sucesso", System.Net.HttpStatusCode.Created);
        });

    }
}