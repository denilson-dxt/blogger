using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Errors;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace API.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await _handleException(context, e);
        }
    }
    private async Task _handleException(HttpContext context, Exception e)
    {
        object error = null;
        switch (e)
        {
            case ApiException exception:
                {
                    _logger.LogError(exception, "API ERROR");
                    context.Response.StatusCode = exception.StatusCode;
                    //_logger.LogError(exception, "API ERROR");
                    error = exception.Errors;
                }
                break;
            case Exception:
                {
                    _logger.LogError(e, "SERVER ERROR");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    //_logger.LogError(e, "SERVER ERROR");

                    error = e.Message;
                }
                break;
        }
        context.Response.ContentType = "application/json";
        var response = JsonSerializer.SerializeToDocument(error);
        await context.Response.WriteAsJsonAsync(response);
    }
}
