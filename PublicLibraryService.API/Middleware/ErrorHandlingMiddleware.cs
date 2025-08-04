using FluentValidation;
using System.Net;
using System.Text.Json;

namespace PublicLibraryService.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

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
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error on path: {Path}", context.Request.Path);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var validationErrors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                var result = JsonSerializer.Serialize(new
                {
                    message = "Validation failed",
                    errors = validationErrors,
                    traceId = context.TraceIdentifier
                });

                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception on path: {Path}", context.Request.Path);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new
                {
                    message = "An unexpected error occurred",
                    traceId = context.TraceIdentifier
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
    
}

