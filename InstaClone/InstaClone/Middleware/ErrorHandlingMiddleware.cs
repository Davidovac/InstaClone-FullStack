using InstaClone.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace InstaClone.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (status, type, title) = exception switch
            {
                BadRequestException => (StatusCodes.Status400BadRequest, "errors.bad-request", "Bad Request"),
                ValidationException => (StatusCodes.Status400BadRequest, "errors.bad-request", "Bad Request"),
                NotFoundException => (StatusCodes.Status404NotFound, "errors.not-found", "Not Found"),
                ForbiddenException => (StatusCodes.Status403Forbidden, "errors.forbidden", "Forbidden"),
                _ => (StatusCodes.Status500InternalServerError, "errors.internal", "Internal Server Error"),
            };

            if (status >= StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(exception, "Unhandled exception for {Method} {Path}", context.Request.Method, context.Request.Path);
            }
            else
            {
                _logger.LogWarning(exception, "Handled domain exception for {Method} {Path}", context.Request.Method, context.Request.Path);
            }

            var details = new ProblemDetails
            {
                Status = status,
                Type = type,
                Title = title,
                Detail = status >= StatusCodes.Status500InternalServerError
                    ? "An unexpected error occurred."
                    : exception.Message,
                Instance = context.Request.Path,
            };

            details.Extensions["traceId"] = context.TraceIdentifier;

            context.Response.StatusCode = status;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(details));
        }
    }
}
