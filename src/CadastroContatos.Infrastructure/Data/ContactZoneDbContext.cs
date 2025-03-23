using CadastroContatos.Domain.Domain;
using CadastroContatos.Infrastructure.Data.FluentMap;
using Microsoft.EntityFrameworkCore;

namespace CadastroContatos.Infrastructure.Data
{
    public class ContactZoneDbContext : DbContext
    {
        public DbSet<ContactDomain> Contatos { get; set; }

        public ContactZoneDbContext(DbContextOptions<ContactZoneDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactMap());
        }
    }
}