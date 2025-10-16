using Cineflow.dtos.pessoas;
using Cineflow.models.pessoas;

namespace CineFlow.Endpoints;

public static class PessoaEndpoints
{
    public static void MapPessoaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/pessoa", async (CriarPessoaDto pessoa) => 
        {
            


            // Lógica para criar um novo cliente
            return Results.Created($"/clientes/{cliente.Id}", cliente);
        });

    }
}