using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using KDramaSystem.Application.Exceptions;
using System.Text.Json;

namespace WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, "Erro de domínio");
                await WriteErrorAsync(context, ex.Message, StatusCodes.Status400BadRequest, ex);
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation(ex, "Recurso não encontrado");
                await WriteErrorAsync(context, ex.Message, StatusCodes.Status404NotFound, ex);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Erro de validação");
                await WriteErrorAsync(context, ex.Message, StatusCodes.Status422UnprocessableEntity, ex);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado");
                await WriteErrorAsync(context, "Erro interno no servidor.", StatusCodes.Status500InternalServerError, ex);
            }
        }

        private async Task WriteErrorAsync(HttpContext context, string message, int statusCode, System.Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new
            {
                statusCode,
                error = message,
                stackTrace = _env.IsDevelopment() ? exception.StackTrace : null
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}