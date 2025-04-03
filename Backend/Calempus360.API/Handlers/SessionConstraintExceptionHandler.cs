using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Calempus360.Errors.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Handlers
{
    public class SessionConstraintExceptionHandler(ILogger<SessionConstraintExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not SessionConstraintException ex) return false;

            logger.LogError("Session constraint not respected : {Exception}", ex.Message);

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "https://httpstatuses.com/400",
                Title = "Session constraint error",
                Detail = exception.Message // Masquer en prod si besoin
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);

            return true; // Indique que l'exception a été gérée
        }
    }

    public class ItemNotAvailableExceptionHandler(ILogger<ItemNotAvailableExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not ItemNotAvailableException ex) return false;

            logger.LogError("Item not available : {Exception}", ex.Message);

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.Conflict,
                Type = "https://httpstatuses.com/409",
                Title = "Session constraint error",
                Detail = exception.Message // Masquer en prod si besoin
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);

            return true; // Indique que l'exception a été gérée
        }
    }
}