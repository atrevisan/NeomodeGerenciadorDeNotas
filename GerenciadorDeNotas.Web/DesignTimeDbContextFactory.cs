using GerenciadorDeNotas.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GerenciadorDeNotas.Web
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GerenciadorDeNotasContexto>
    {
        public GerenciadorDeNotasContexto CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<GerenciadorDeNotasContexto>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new GerenciadorDeNotasContexto(builder.Options);
        }
    }
}
