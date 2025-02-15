using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Handlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is not { } ex) return false;
        
        logger.LogError("Not handled exception : {Exception}", ex.Message);

        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Type   = "https://httpstatuses.com/500",
            Title  = "An error occurred while processing your request",
            Detail = exception.Message // Masquer en prod si besoin
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode  = problemDetails.Status.Value;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);

        return true; // Indique que l'exception a été gérée
    }
}