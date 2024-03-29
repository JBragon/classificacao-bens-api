using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using DataAccess.Context;
using WebApi.Configurations;
using FluentValidation.AspNetCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HostBuilder>(host =>
{
    string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    host.ConfigureAppConfiguration((hostingContext, config) =>
    {
        if (string.IsNullOrEmpty(env))
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        else
            config.AddJsonFile($"appsettings.{env}.json", optional: false);

        config.AddEnvironmentVariables();

    });
});

builder.Services.AddEntityFrameworkMySQL().AddDbContext<DBContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("DBConnectionString"));
}).AddUnitOfWork<DBContext>();

builder.Services.AddInfrastructure();
builder.Services.RegisterMapper();

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// Add services to the container.
builder.Services.AddControllers()
            .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddHealthChecks()
.AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Classificacao Bens API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
              new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()

        }
    });
});

var app = builder.Build();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UseHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

//Necessário para o uso dos teste de integração
public partial class Program { }
