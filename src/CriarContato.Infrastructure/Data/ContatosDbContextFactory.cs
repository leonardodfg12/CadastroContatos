using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CriarContato.Infrastructure.Data
{
    public class ContatosDbContextFactory : IDesignTimeDbContextFactory<ContactZoneDbContext>
    {
        public ContactZoneDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContactZoneDbContext>();
            
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "CriarContato.Producer.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new ContactZoneDbContext(optionsBuilder.Options);
        }
    }
}