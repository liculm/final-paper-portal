using FinalPaper.Domain.Entities;
using FinalPaper.Domain.Interfaces;
using Microsoft.Extensions.Primitives;

namespace Api.Middlewares;

public sealed class UserMiddleware
{
    readonly RequestDelegate next;

    public UserMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext httpContext, IConfiguration configuration,
        IJwtService jwtService)
    {
        if (httpContext.Request.Headers.TryGetValue("Authorization", out StringValues bearerToken))
        {
            var jwtToken = bearerToken.ToString().Replace("Bearer ", "", StringComparison.Ordinal);
            if (string.IsNullOrEmpty(jwtToken))
            {
                await next.Invoke(httpContext);
                return;
            }

            var user = jwtService.ReadJwtToken<User>(jwtToken, true);
            if (user is not null)
            {
                var currentUser = httpContext.RequestServices.GetRequiredService<User>();
                currentUser.UpdateUser(user);
            }
        }

        await next.Invoke(httpContext);
    }
}