using System.Net;
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
public static class ClienteEndpoints
{

    public static void MapClienteEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapPost("/cliente",
            async ([FromServices] IClienteService _pessoaService, [FromBody] CriarClienteDto pessoa) =>
            {
                var result = await _pessoaService.AddClienteAsync(pessoa);

                if (!result.IsSuccess)
                {
                    return result.ToActionResult(result.Error, null, System.Net.HttpStatusCode.BadRequest);
                }

                return result.ToActionResult(null, "Usuário criado com sucesso", System.Net.HttpStatusCode.Created);
            });

        app.MapGet("/allClientes", async ([FromServices] IClienteService _pessoaService) =>
        {
            var result = await _pessoaService.ReturnAllClientesAsync();
            if (!result.IsSuccess)
            {
                return result.ToActionResult(result.Error, null, System.Net.HttpStatusCode.BadRequest);
            }

            return result.ToActionResult(null, "Usuários retornados com sucesso", System.Net.HttpStatusCode.OK);

        });

        app.MapDelete("/deleteCliente", async ([FromServices] IClienteService _pessoaService,
            [FromQuery] string idCliente) =>
        {
            var result = await _pessoaService.DeleteClienteAsync(idCliente);

            if (!result.IsSuccess)
            {
                return result.ToActionResult(result.Error, null, HttpStatusCode.BadRequest);
            }
            // verificar amanha a questao do result

            return result.ToActionResult(null, "Delete realizado com sucesso", HttpStatusCode.NoContent);
        });

        app.MapPut("/putCliente", async ([FromServices] IClienteService _pessoaService, [FromBody] CriarClienteDto dto
            ,[FromQuery] Guid ID) =>
        {
            var result = await _pessoaService.PutClienteAsync(ID, dto);

            if (!result.IsSuccess)
            {
                return result.ToActionResult(result.Error, null, HttpStatusCode.BadRequest);
            }

            return result.ToActionResult
                (null, "Usuario atualizado com sucesso.", HttpStatusCode.OK);

        });
    }
}