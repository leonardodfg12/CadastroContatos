using CadastroContatos.Application.DTOs;
using CadastroContatos.Domain.Domain;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CadastroContatos.Application.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IBus _bus;
        private readonly ILogger<ContatoService> _logger;

        public ContatoService(IBus bus, ILogger<ContatoService> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async void EnviarContatoParaFila(ContatoDto? contatoDto)
        {
            var mensagem = new ContatoMessage
            {
                Id = Guid.NewGuid(),
                Name = contatoDto.Name,
                DDD = contatoDto.DDD,
                Phone = contatoDto.Phone,
                Email = contatoDto.Email
            };

            _logger.LogInformation("Sending message to queue: cadastro-queue");

            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:cadastro-queue"));
            await endpoint.Send(mensagem);

            _logger.LogInformation("Message sent successfully: {MessageId}", mensagem.Id);
        }
    }
}