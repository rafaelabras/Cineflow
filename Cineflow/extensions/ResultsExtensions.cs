using Cineflow.commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Runtime.CompilerServices;

namespace Cineflow.extensions
{
    public static class ResultsExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(ServiceResponse<T>.Ok(result.Value, result.SucessMessageUser));
            }

            return new BadRequestObjectResult(ServiceResponse<T>.Fail(result.ErrorMessageUser ?? "Ocorreu um erro"));

        }

    }
}
