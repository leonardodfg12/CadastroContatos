using CriarContato.Domain.Domain;
using CriarContato.Infrastructure.Data;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CriarContato.Infrastructure.Consumers;

public class ContatoConsumer : IConsumer<ContactDomain>
{
    private readonly ContactZoneDbContext _context;
    private readonly ILogger<ContatoConsumer> _logger;

    public ContatoConsumer(ContactZoneDbContext context, ILogger<ContatoConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ContactDomain> context)
    {
        var contatoMessage = context.Message;
        
        var contato = new ContactDomain
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