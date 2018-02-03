using GerenciadorDeNotas.Dados.Mapeamento;
using GerenciadorDeNotas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeNotas.Dados
{
    public class GerenciadorDeNotasContexto : DbContext
    {
        
        public GerenciadorDeNotasContexto(DbContextOptions<GerenciadorDeNotasContexto> options) : base(options)
        {
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new AlunoMapa(modelBuilder.Entity<Aluno>());
            new AvaliacaoMapa(modelBuilder.Entity<Avaliacao>());
            new CidadeMapa(modelBuilder.Entity<Cidade>());
        }
    }
}
