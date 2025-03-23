using CriarContato.Domain.Domain;
using CriarContato.Infrastructure.Data.FluentMap;
using Microsoft.EntityFrameworkCore;

namespace CriarContato.Infrastructure.Data
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