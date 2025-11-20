using System.Net;
using Cineflow.dtos.cinema;
using Cineflow.extensions;
using Cineflow.@interface.CinemaInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;

namespace CineFlow.Endpoints;

public static class IngressoEndpoint
{
    public static void MapIngressoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/ingresso", async ([FromServices] IIngressoService service, [FromBody] CriarIngressoDto criarIngressoDto) =>
        {
            var post = await service.CriarIngressoAsync(criarIngressoDto);

            if (!post.IsSuccess)
                return post.ToActionResult("Não foi possível criar o ingresso", null, HttpStatusCode.BadRequest);
            
            return post.ToActionResult(null, "Ingresso criado com sucesso!", HttpStatusCode.Created);
        });
        
        app.MapGet("/ingresso", async ([FromServices] IIngressoService service, Guid id,
            string? id_sessao,
            int? id_assento,
            string? id_reserva,
            int? id_sala,
            int? id_filme,
            decimal Preco,
            string? Codigo_qr,
            DateTime? Data_gerado,
            DateTime? Data_validacao,
            Boolean? Utilizado) =>
        {

            var filtro = new IngressoFiltroDto
            {   ID = id,
                Id_sessao = id_sessao,
                Id_assento = id_assento,
                Id_reserva = id_reserva,
                Id_sala = id_sala,
                Id_filme = id_filme,
                preco = Preco,
                codigo_qr = Codigo_qr,
                data_gerado = Data_gerado,
                data_validacao = Data_validacao,
                utilizado = Utilizado };
            
            var query = await service.GetIngressoAsync(filtro);
            
            if (!query.IsSuccess)
                return query.ToActionResult("Não foi possível retornar o(s) ingresso(s)", null, HttpStatusCode.BadRequest); 
            
            return query.ToActionResult(null, "Ingresso(s) retornado com sucesso!", HttpStatusCode.OK);
        });
        
        
        
        app.MapDelete(("/ingresso"), async([FromServices] IIngressoService service, [FromQuery] Guid ingressoId) =>
        {
            var delete = await service.DeleteIngressoAsync(ingressoId);

            if (!delete.IsSuccess && delete.Value == false)
                return delete.ToActionResult("Não foi possível deletar o ingresso", null, HttpStatusCode.BadRequest);
            
            return delete.ToActionResult(null, "Ingresso deletado com sucesso!", HttpStatusCode.NoContent);
        });

        app.MapPut("/ingresso", async ([FromServices]IIngressoService service, [FromBody] CriarIngressoDto criarIngressoDto, [FromQuery] Guid id) =>
        {
            
            var put = await service.CriarIngressoAsync(criarIngressoDto);
            
            if (!put.IsSuccess)
                put.ToActionResult("Não foi possível atualizar o ingresso",null, HttpStatusCode.BadRequest);

            put.ToActionResult(null, "Ingresso atualizado com sucesso!", HttpStatusCode.OK);

        });


    }
    
    
}