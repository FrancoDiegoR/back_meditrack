using System.Net;
using System.Text.Json;

namespace TechnoByteLambders.MediTrackSensor.Platform.Shared.Infrastructure.Interfaces.ASP.Configuration;

/// <summary>
/// Middleware that catches unhandled exceptions and returns a ProblemDetails-style JSON response.
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
            _logger.LogError(exception, "Unhandled exception while processing {Path}", context.Request.Path);
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            ArgumentException => HttpStatusCode.BadRequest,
            InvalidOperationException => HttpStatusCode.BadRequest,
            KeyNotFoundException => HttpStatusCode.NotFound,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        var problem = new
        {
            type = $"https://httpstatuses.io/{(int)statusCode}",
            title = statusCode.ToString(),
            status = (int)statusCode,
            detail = exception.Message,
            instance = context.Request.Path.Value
        };

        var payload = JsonSerializer.Serialize(problem);
        return context.Response.WriteAsync(payload);
    }
}

/// <summary>
/// Extension methods to register the <see cref="GlobalExceptionHandlerMiddleware"/> in the request pipeline.
/// </summary>
public static class GlobalExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
