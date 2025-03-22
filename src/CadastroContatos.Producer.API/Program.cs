using CadastroContatos.Application.Services;
using CadastroContatos.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);

// Configurar MassTransit via Infrastructure
builder.Services.AddMassTransitConfiguration(builder.Configuration);

// Adicionar serviços ao container
builder.Services.AddScoped<IContatoService, ContatoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();