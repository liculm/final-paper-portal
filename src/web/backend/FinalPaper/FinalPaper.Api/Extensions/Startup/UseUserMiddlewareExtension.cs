using Api.Middlewares;

namespace Api.Extensions.Startup;

public static class UseUserMiddlewareExtension
{
    public static IApplicationBuilder UseUserMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<UserMiddleware>();
        return app;
    }
}