using System.Text.Json;
using FluentValidation;

namespace TennisGame.Api.Middlewares;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            
            response.StatusCode = error switch
            {
                ValidationException =>
                    StatusCodes.Status400BadRequest,
                ArgumentException =>
                    StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var result = JsonSerializer.Serialize(new ErrorInfo(error?.Message ?? "Internal error", error?.ToString()));
            await response.WriteAsync(result);
        }
    }
}

public record ErrorInfo(string Message, string? Trace);