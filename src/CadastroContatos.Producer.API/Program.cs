using CadastroContatos.Application.Dtos;
using CadastroContatos.Application.Dtos.Validators;
using CadastroContatos.Application.Services;
using CadastroContatos.Infrastructure.Config;
using CadastroContatos.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

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

// Configuração do middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CadastroContatos API V1"));
}

app.UseAuthorization();
app.MapControllers();
app.Run();