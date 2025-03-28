using System.Diagnostics.CodeAnalysis;
using CriarContato.Infrastructure.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CriarContato.Infrastructure.Config
{
    [ExcludeFromCodeCoverage]
    public static class MassTransitConfig
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqHost = configuration["RABBITMQ_HOST"];

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ContatoConsumer>();
                
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqHost);
                    
                    cfg.ReceiveEndpoint("criar-contato-queue", e =>
                    {
                        e.ConfigureConsumer<ContatoConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}