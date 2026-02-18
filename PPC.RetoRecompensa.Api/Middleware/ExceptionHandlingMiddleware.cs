using System.Net;
using PPC.RetoRecompensa.Application.Contracts;
using PPC.RetoRecompensa.Domain.Exceptions;

namespace PPC.RetoRecompensa.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
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
        catch (ValidationException vex)
        {
            _logger.LogError($"Ocurrió una excepción de validación: {vex.Message}");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/problem+json";
            var problem = new RespuestaDto
            {
                EsCorrecto = false,
                Mensaje = vex.Message
            };
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (BusinessException dex)
        {
            _logger.LogError($"Ocurrió una excepción de negocio: {dex.Message}");
            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity; // 422
            context.Response.ContentType = "application/problem+json";
            var problem = new RespuestaDto
            {
                EsCorrecto = false,
                Mensaje = dex.Message
            };
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocurrió una excepción inesperada: {ex.ToString()}");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/problem+json";
            var problem = new RespuestaDto
            {
                EsCorrecto = false,
                Mensaje = "Ocurrió una excepción inesperada"//ex.Message
            };
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}