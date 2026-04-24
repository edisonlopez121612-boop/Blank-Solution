using System.Net;
using System.Text.Json;
using BluesoftBank.Domain.Excepciones;

namespace BluesoftBank.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            _logger.LogError(ex, "Error no controlado");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        HttpStatusCode status;
        string message;

        switch (exception)
        {
            case CuentaNoExisteException:
                status = HttpStatusCode.NotFound;
                message = exception.Message;
                break;

            case SaldoInsuficienteException:
            case ValorNoPermitidoException:
            case InvalidOperationException:
                status = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            case ArgumentException:
                status = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            default:
                status = HttpStatusCode.InternalServerError;
                message = "Error interno del servidor";
                break;
        }

        context.Response.StatusCode = (int)status;

        var response = new
        {
            error = message,
            status = (int)status
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}