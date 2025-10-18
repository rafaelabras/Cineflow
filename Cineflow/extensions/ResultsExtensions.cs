using Cineflow.commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Runtime.CompilerServices;

namespace Cineflow.extensions
{
    public static class ResultsExtensions
    {
        public static IResult ToActionResult<T>(
            this Result<T> result,
            string? errorMessageUser,
            string? sucessMessageUser,
            HttpStatusCode? overrideStatus = null
        )
        {
            var response = result.IsSuccess 
                ? ServiceResponse<T>.Ok(result.Value, sucessMessageUser)
                : ServiceResponse<T>.Fail(errorMessageUser ?? "Ocorreu um erro");


            // é obrigatorio definir um status code no endpoint devido a lógica criada de retorno
            var statuscode = overrideStatus ?? (result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.BadRequest);


            if (!overrideStatus.HasValue) {

                var error = new ServiceResponse<T>(false, default , "Ocorreu um erro", "");

                return Results.Json(error, statusCode: 400);
            }

                return Results.Json(response, statusCode: (int)statuscode);
           


        }
    }
}
