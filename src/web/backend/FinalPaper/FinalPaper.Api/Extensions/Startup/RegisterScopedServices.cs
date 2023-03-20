using Api.Behaviors;
using FinalPaper.Domain.Utility;
using MediatR;

namespace Api.Extensions.Startup; 

public static class RegisterScopedServices
{
    public static IServiceCollection RegisterScoped(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            // .AddScoped<IJwtEncryptionService, JwtEncryptionService>()
            .AddScoped<IDateTime, DefaultDateTime>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}