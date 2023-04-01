using System.Reflection;
using System.Text;
using Api.Extensions.Startup;
using FinalPaper.Command.CommandHandlers.LoginCommand;
using FinalPaper.Infrastructure;
using FinalPaper.Query.QueryHandlers.GetAllUsers;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
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

builder.Services.AddCors(options => {
    options.AddPolicy("final-paper-api",
        builder => {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
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

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblies(new[]
    {
        typeof(GetAllUsersQuery).Assembly,
        typeof(LoginCommand).Assembly
    });

var commandAssembly = Assembly.GetAssembly(typeof(LoginCommandHandler));
var queryAssembly = Assembly.GetAssembly(typeof(GetAllUsersQuery));
if (commandAssembly != null && queryAssembly != null)
    builder.Services.AddMediatR(commandAssembly, queryAssembly);

builder.Services.AddLogging()
    .AddMemoryCache()
    .RegisterDbContext<FinalPaperDBContext>(builder.Configuration)
    .RegisterScoped(builder.Configuration)
    .AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("final-paper-api");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();