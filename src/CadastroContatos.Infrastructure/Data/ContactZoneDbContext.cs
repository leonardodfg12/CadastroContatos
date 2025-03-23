using CadastroContatos.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace CadastroContatos.Infrastructure.Data;

public class ContactZoneDbContext : DbContext
{
    public ContactZoneDbContext(DbContextOptions<ContactZoneDbContext> options) : base(options) { }
    public DbSet<ContactDomain> Contatos { get; set; }
}
