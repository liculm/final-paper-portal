using FinalPaper.Domain.Entities;

namespace Api.Extensions.Startup;

public static class CurrentUser
{
    public static IServiceCollection AddCurrentUser(this IServiceCollection services)
    {
        services.AddScoped<User>();
        return services;
    }
}