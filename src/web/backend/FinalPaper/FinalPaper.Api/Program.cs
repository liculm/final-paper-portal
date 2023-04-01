using System.Text;
using Api.Extensions.Startup;
using FinalPaper.Domain.Interfaces;
using FinalPaper.Infrastructure;
using FinalPaper.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddControllers();
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
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your_issuer",
            ValidAudience = "your_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("final_paper_portal"))
        };
    });

builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IJwtService, JwtService>();


// builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
// builder.Services
//     .AddControllers()
//     .AddFluentValidation(fv =>
//     {
//         fv.RegisterValidatorsFromAssemblyContaining(typeof(GetProjectsQuery));
//         fv.RegisterValidatorsFromAssemblyContaining(typeof(CreateProjectsCommand));
//     })
//     .AddShipsJsonOptions();
//
// var commandAssembly = Assembly.GetAssembly(typeof(GetProjectsQueryHandler));
// var queryAssembly = Assembly.GetAssembly(typeof(CreateProjectsCommandHandler));
// if (commandAssembly != null && queryAssembly != null)
//     builder.Services.AddMediator(commandAssembly, queryAssembly);

builder.Services.AddLogging()
    .AddMemoryCache()
    .RegisterDbContext<FinalPaperDBContext>(builder.Configuration)
    .RegisterScoped(builder.Configuration)
    // .AddCurrentADUser()
    // .AddVersioningAndSwagger(config =>
    // {
    //     config.Add(Path.Combine(AppContext.BaseDirectory, "FinalPaper.Api.xml"));
    //     config.Add(Path.Combine(AppContext.BaseDirectory, "FinalPaper.Command.xml"));
    //     config.Add(Path.Combine(AppContext.BaseDirectory, "FinalPaper.Query.xml"));
    //     config.Add(Path.Combine(AppContext.BaseDirectory, "FinalPaper.Infrastructure.xml"));
    //     config.Add(Path.Combine(AppContext.BaseDirectory, "FinalPaper.Domain.xml"));
    // })
    ;

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("final-paper-api");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();