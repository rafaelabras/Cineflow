using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cineflow.middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger  = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            HandleExceptionAsync(httpContext, e);
            throw;
        }
    }

    public async void HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        _logger.LogWarning($"{exception} | An exception occurred | TraceId: {httpContext.TraceIdentifier}");

        var (status, message) = exception switch
        {
            DbException => (StatusCodes.Status500InternalServerError, exception.Message),
            Exception => (StatusCodes.Status400BadRequest, exception.Message)
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = status;

        var response = new
        {
            message = message,
            traceId = httpContext.TraceIdentifier
        };
        
        await httpContext.Response.WriteAsJsonAsync(response);
    }
}