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

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception e)
        {
            await _handleException(context, e);
        }
    }
    private async Task _handleException(HttpContext context, Exception e)
    {
        object error = null;
        switch(e)
        {
            case ApiException exception:
            {
                context.Response.StatusCode = exception.StatusCode;
                error = exception.Errors;
            }
            break;
            default:
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                error = e.Message;
            }
            break;
        }
        context.Response.ContentType = "application/json";
        var response = JsonSerializer.SerializeToDocument(error);
        await context.Response.WriteAsJsonAsync(response);
    }
}