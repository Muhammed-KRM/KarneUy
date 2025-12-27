using Karne.API.Services;
using System.Net;
using System.Text.Json;

namespace Karne.API.Middleware
{
    /// <summary>
    /// Middleware to handle global exceptions and log them to the database.
    /// Ensures the API returns a standard JSON error response instead of crashing or showing HTML.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger; // Default framework logger

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                // We need to resolve the Scoped service (ILoggingService) within the singleton middleware pipeline
                // by creating a scope.
                using (var scope = context.RequestServices.CreateScope())
                {
                    var dbLogger = scope.ServiceProvider.GetRequiredService<ILoggingService>();
                    await dbLogger.LogErrorAsync($"Global Exception: {ex.Message}", ex, context.Request.Path);
                }

                // Also log to console/file via default logger
                _logger.LogError(ex, "An unhandled exception has occurred.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware.",
                Detailed = exception.Message // In production, hide this.
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
