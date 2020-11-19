using Doctrina.ExperienceApi.Data.Exceptions;
using Doctrina.ExperienceApi.Server.Exceptions;
using Doctrina.ExperienceApi.Server.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Routing
{
    /// <summary>
    /// xAPI Exception Middleware
    /// Catches all xAPI related exception, and return JSON
    /// </summary>
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<ApiExceptionMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                if (ex is TaskCanceledException)
                {
                    logger.LogError(ex, "Request was cancelled");
                    return;
                }
                else if (typeof(StatusCodeException).IsAssignableFrom(ex.GetType()))
                {
                    var exception = (StatusCodeException)ex;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)exception.StatusCode;

                    if (ex is ValidationException)
                    {
                        await context.Response.WriteJsonAsync(new { failures = ((ValidationException)ex).Failures });
                    }
                    else
                    {
                        await context.Response.WriteJsonAsync(new { message = ex.Message });
                    }
                    return;
                }
                else if (ex is BadRequestException
                    || ex is IOException /* Invalid formattet HTTP requests */
                    || ex is InvalidDataException /* Form section has invalid Content-Disposition value */)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                else if (ex is NotFoundException)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteJsonAsync(new { message = ex.Message });
                    return;
                }
                else
                {
                    logger.LogError(ex, "Exception was thrown");

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteJsonAsync(new
                    {
                        error = new[] { ex.InnerException?.Message ?? ex.Message },
                        type = ex.GetType().Name,
                        stackTrace = ex.StackTrace
                    });
                    return;
                }
            }

            // We cannot modify response headers after
        }
    }
}
