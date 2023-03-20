using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Api.Extensions.Startup; 

public static class EntityFramework
{
    public static IServiceCollection RegisterDbContext<T>(this IServiceCollection services,
        IConfiguration configuration) where T : DbContext
    {
        services
            .AddDbContext<T>(options =>
                {
                    options.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                }
            );

        return services;
    }
}