using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CadastroContatos.Infrastructure.Data
{
    public class ContatosDbContextFactory : IDesignTimeDbContextFactory<ContatosDbContext>
    {
        public ContatosDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContatosDbContext>();
            
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "CadastroContatos.Producer.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new ContatosDbContext(optionsBuilder.Options);
        }
    }
}