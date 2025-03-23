using CadastroContatos.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace CadastroContatos.Infrastructure.Data;

public class ContatosDbContext : DbContext
{
    public ContatosDbContext(DbContextOptions<ContatosDbContext> options) : base(options) { }
    public DbSet<ContatoMessage> Contatos { get; set; }
}
