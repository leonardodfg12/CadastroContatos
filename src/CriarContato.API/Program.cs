using System.Diagnostics.CodeAnalysis;
using CriarContato.Application.Dtos;
using CriarContato.Application.Dtos.Validators;
using CriarContato.Application.Services;
using CriarContato.Infrastructure.Config;
using CriarContato.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace CriarContato.API;

public partial class Program
{
    [ExcludeFromCodeCoverage]
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        // Configura a conexão com o banco de dados com EnableRetryOnFailure
        builder.Services.AddDbContext<ContactZoneDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

        // Configurar MassTransit via Infrastructure
        builder.Services.AddMassTransitConfiguration(builder.Configuration);

        // Adicionar serviços ao container
        builder.Services.AddScoped<IContatoService, ContatoService>();
        builder.Services.AddTransient<IValidator<PostContactDto>, PostContactDtoValidator>();

        // Add FluentValidation services
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Garante que o banco e as tabelas existam antes de iniciar a aplicação
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ContactZoneDbContext>();
            dbContext.Database.EnsureCreated();
        }

        // Configuração do middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CriarContato API V1"));
        }

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}