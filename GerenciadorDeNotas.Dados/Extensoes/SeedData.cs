using GerenciadorDeNotas.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciadorDeNotas.Dados.Extensoes
{
    public static class SeedData
    {
        public static void Seed(this IServiceProvider serviceProvider)
        {
            using (var context = new GerenciadorDeNotasContexto(
                serviceProvider.GetRequiredService<DbContextOptions<GerenciadorDeNotasContexto>>()))
            {
                
                if (!context.Set<Cidade>().Any())
                {
                    context.Set<Cidade>().AddRange(
                     new Cidade
                     {
                         Nome = "Curitiba",
                         UF = "PR"
                     }
                    );

                    context.SaveChanges();
                }
                
                if (!context.Set<Aluno>().Any())
                {
                    List<Aluno> alunos = new List<Aluno>();
                    for (int i = 0; i < 100; i++)
                    {
                        Aluno aluno = new Aluno
                        {
                            NomeCompleto = MockData.Person.FullName(),
                            Matricula = Guid.NewGuid().ToString().Substring(0, 10),
                            EMail = MockData.Internet.Email()
                           
                        };
                        alunos.Add(aluno);
                    }

                    context.Set<Aluno>().AddRange(alunos);
                    context.SaveChanges();

                    foreach(Aluno aluno in alunos)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            context.Set<Avaliacao>().Add(new Avaliacao { AlunoId = aluno.ID, Nota = MockData.RandomNumber.Next(0, 10)});
                        }
                    }

                    context.SaveChanges();
                }

                
            }
        }
    }
}
