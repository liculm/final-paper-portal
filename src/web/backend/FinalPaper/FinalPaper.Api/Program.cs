using Api.Extensions.Startup;
using FinalPaper.Infrastructure;
using Microsoft.AspNetCore.Mvc;

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

app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();