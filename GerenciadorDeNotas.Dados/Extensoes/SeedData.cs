using GerenciadorDeNotas.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GerenciadorDeNotas.Dados.Extensoes
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GerenciadorDeNotasContexto(
                serviceProvider.GetRequiredService<DbContextOptions<GerenciadorDeNotasContexto>>()))
            {
                
                if (context.Set<Cidade>().Any())
                {
                    return;   // DB has been seeded
                }

                context.Set<Cidade>().AddRange(
                     new Cidade
                     {
                         Nome = "Curitiba",
                         UF = "PR"
                     }
                );
                context.SaveChanges();
            }
        }
    }
}
