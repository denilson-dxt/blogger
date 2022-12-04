using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;

namespace API.Middlewares;
public class AuthenticatedUserGetterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserService _userService;

    public AuthenticatedUserGetterMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context, IUserService userService)
    {
        if (context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier) != null)
        {
            var id = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            Console.WriteLine(id);
            userService.SetActualUserId(id);

        }

        await _next(context);
    }
}