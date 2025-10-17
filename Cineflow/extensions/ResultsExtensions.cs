using Cineflow.commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Runtime.CompilerServices;

namespace Cineflow.extensions
{
    public static class ResultsExtensions
    {
        public static IActionResult ToActionResult<T>(
            this Result<T> result,
            string? errorMessageUser,
            string? sucessMessageUser,
            HttpStatusCode? overrideStatus = null
        )
        {
            var response = result.IsSuccess 
                ? ServiceResponse<T>.Ok(result.Value, sucessMessageUser)
                : ServiceResponse<T>.Fail(errorMessageUser ?? "Ocorreu um erro");

            var statusCode = overrideStatus ?? (result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.BadRequest);

            if (!overrideStatus.HasValue) {
                return new ObjectResult("Erro interno do servidor")
                {
                    StatusCode = 400
                };
            }

            return new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
