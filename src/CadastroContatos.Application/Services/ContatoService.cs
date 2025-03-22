using CadastroContatos.Application.DTOs;
using CadastroContatos.Domain.Domain;
using MassTransit;

namespace CadastroContatos.Application.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IBus _bus;

        public ContatoService(ISendEndpointProvider sendEndpointProvider, IBus bus)
        {
            _bus = bus;
        }

        public async void EnviarContatoParaFila(ContatoDto? contatoDto)
        {
            if (contatoDto != null)
            {
                var mensagem = new ContatoMessage
                {
                    Id = Guid.NewGuid(),
                    Name = contatoDto.Name,
                    DDD = contatoDto.DDD,
                    Phone = contatoDto.Phone,
                    Email = contatoDto.Email
                };
                
                var endpoint = await _bus.GetSendEndpoint(new Uri("queue:cadastro-queue"));
                await endpoint.Send(mensagem);
            }
        }
    }
}