using CadastroContatos.Domain.Domain;
using CadastroContatos.Infrastructure.Data;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CadastroContatos.Infrastructure.Consumers;

public class ContatoConsumer : IConsumer<ContatoMessage>
{
    private readonly ContatosDbContext _context;
    private readonly ILogger<ContatoConsumer> _logger;

    public ContatoConsumer(ContatosDbContext context, ILogger<ContatoConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ContatoMessage> context)
    {
        var contatoMessage = context.Message;
        
        var contato = new ContatoMessage
        {
            Id = contatoMessage.Id,
            Name = contatoMessage.Name,
            DDD = contatoMessage.DDD,
            Phone = contatoMessage.Phone,
            Email = contatoMessage.Email
        };
        
        _logger.LogInformation("Received message: {MessageId}", contato.Id);
        _context.Contatos.Add(contato);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Message saved successfully: {MessageId}", contato.Id);
    }
}