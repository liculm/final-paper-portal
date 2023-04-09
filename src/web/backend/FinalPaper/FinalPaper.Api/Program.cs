using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Api.Extensions.Startup;
using Api.Middlewares;
using FinalPaper.Command.CommandHandlers.Authentication.Login;
using FinalPaper.Infrastructure;
using FinalPaper.Query.QueryHandlers.GetAllUsers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });

builder.Services.AddValidatorsFromAssemblyContaining(typeof(GetAllUsersQuery));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(LoginCommand));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("final-paper-api",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrEmpty(builder.Configuration["Jwt:Issuer"]),
            ValidateAudience = !string.IsNullOrEmpty(builder.Configuration["Jwt:Audience"]),
            ValidIssuer = string.IsNullOrEmpty(builder.Configuration["Jwt:Issuer"])
                ? null
                : builder.Configuration["Jwt:Issuer"],
            ValidAudience = string.IsNullOrEmpty(builder.Configuration["Jwt:Audience"])
                ? null
                : builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
                                                                throw new InvalidOperationException()))
        };
    });

var commandAssembly = Assembly.GetAssembly(typeof(LoginCommandHandler));
var queryAssembly = Assembly.GetAssembly(typeof(GetAllUsersQuery));
if (commandAssembly != null && queryAssembly != null)
    builder.Services.AddMediatR(commandAssembly, queryAssembly);

builder.Services.AddLogging()
    .AddMemoryCache()
    .RegisterDbContext<FinalPaperDBContext>(builder.Configuration)
    .RegisterScoped(builder.Configuration)
    .AddCurrentUser()
    .AddVersioningAndSwagger(config =>
    {
        config.Add(Path.Combine(AppContext.BaseDirectory, "FinalPaper.Api.xml"));
        config.Add(Path.Combine(AppContext.BaseDirectory, "FinalPaper.Command.xml"));
        config.Add(Path.Combine(AppContext.BaseDirectory, "FinalPaper.Query.xml"));
        config.Add(Path.Combine(AppContext.BaseDirectory, "FinalPaper.Infrastructure.xml"));
        config.Add(Path.Combine(AppContext.BaseDirectory, "FinalPaper.Domain.xml"));
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>()
                     .ApiVersionDescriptions)
            c.SwaggerEndpoint(
                builder.Configuration["SwaggerEndpoint"]
                    ?.Replace("{version:apiVersion}", description.GroupName,
                        StringComparison.Ordinal),
                $"API {description.GroupName.ToUpperInvariant()}");
    });
}

app.UseCors("final-paper-api");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseUserMiddleware();
app.MapControllers();

app.Run();