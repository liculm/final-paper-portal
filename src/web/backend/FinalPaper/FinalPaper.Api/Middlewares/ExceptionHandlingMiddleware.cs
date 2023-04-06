using System.Net;
using System.Text.Json;
using Api.Exceptions;
using FluentValidation;

namespace Api.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> logger;
    private readonly RequestDelegate next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    ///     Set the correct HttpStatusCode for a response depending on the exception type
    ///     On top of that, write a helpful response message if we're not dealing with a 500 error
    /// </summary>
    /// <param name="context"></param>
    /// <param name="ex"></param>
    /// <returns></returns>
    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        switch (ex)
        {
            case NotFoundException:
                logger.LogError(ex, "Not found: ");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case ValidationException:
                logger.LogError(ex, "Validation Error: ");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case InvalidOperationException:
                logger.LogError(ex, "Invalid Operation: ");
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                break;
            case UnauthorizedAccessException:
                logger.LogError(ex, "Unauthorized operation: ");
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            default:
                logger.LogCritical(ex, "Critical: ");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.ContentType = "application/json";

        switch (ex)
        {
            case NotFoundException or InvalidOperationException or UnauthorizedAccessException
                or ValidationException:
            {
                var serializedExceptionMessage = JsonSerializer.Serialize(new { message = ex.Message });
                return context.Response.WriteAsync(serializedExceptionMessage);
            }
            default:
                return Task.CompletedTask;
        }
    }
}