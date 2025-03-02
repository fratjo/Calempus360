using System.Net;
using System.Text.Json;
using Calempus360.Errors;
using Calempus360.Errors.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Handlers;

public class ExistingEntityExceptionHandler(ILogger<ExistingEntityExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext       httpContext,
        Exception         exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ExistingEntityException ex) return false;

        logger.LogError("Existing item exception : {Exception}", ex.Message);

        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.Conflict,
            Type   = "https://httpstatuses.com/409",
            Title  = "Already existing item",
            Detail = exception.Message // Masquer en prod si besoin
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode  = problemDetails.Status.Value;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);

        return true; // Indique que l'exception a été gérée
    }
}