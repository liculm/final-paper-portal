using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace Api.Extensions.Startup;

/// <summary>
///     Add Versioning Support and Swagger for the Api
/// </summary>
public static class VersioningAndSwagger
{
	/// <summary>
	///     Add Versioning strategy for the API and add Swagger support based on the version numbers.
	/// </summary>
	/// <param name="services"></param>
	/// <param name="includeCommentsPaths">List of paths for the XML documentation</param>
	/// <returns></returns>
	public static IServiceCollection AddVersioningAndSwagger(this IServiceCollection services,
        Action<IList<string>>? includeCommentsPaths = null)
    {
        services.AddApiVersioning(
            o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                // reports if the api version is deprecated
                o.ReportApiVersions = true;
            });

        // Group the Api versions
        var apiExplorer = services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddSwaggerGen(
            c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                    c.SwaggerDoc(
                        description.GroupName,
                        new OpenApiInfo
                        {
                            Title = $"FinalPaper.Api version {description.ApiVersion}",
                            Version = description.ApiVersion.ToString()
                        });

                includeCommentsPaths = IncludeCommentsInSwagger =>
                {
                    if (includeCommentsPaths != null)
                        foreach (var item in IncludeCommentsInSwagger)
                            c.IncludeXmlComments(item);
                };

                c.CustomSchemaIds(i => i.FullName);
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            Name = "Authorization",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

        return services;
    }
}