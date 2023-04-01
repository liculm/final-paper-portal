using FinalPaper.Domain.Interfaces;
using FinalPaper.Domain.Utility;
using FinalPaper.Infrastructure.Services;
using MediatR;

namespace Api.Extensions.Startup; 

public static class RegisterScopedServices
{
    public static IServiceCollection RegisterScoped(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddScoped<IDateTime, DefaultDateTime>()
            .AddScoped<IRefreshTokenService, RefreshTokenService>()
            .AddScoped<IJwtService, JwtService>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}