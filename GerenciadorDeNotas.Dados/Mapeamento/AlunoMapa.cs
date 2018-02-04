using GerenciadorDeNotas.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeNotas.Dados.Mapeamento
{
    public class AlunoMapa : EntidadeBaseMapa<Aluno>
    {
        public AlunoMapa(EntityTypeBuilder<Aluno> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.Property(m => m.NomeCompleto).IsRequired().HasMaxLength(50);
            entityBuilder.Property(m => m.Matricula).IsRequired().HasMaxLength(10);
            entityBuilder.Property(m => m.Foto).HasMaxLength(50);
            entityBuilder.Property(m => m.Telefone).HasMaxLength(16);
            entityBuilder.Property(m => m.EMail).HasMaxLength(50);
            entityBuilder.Property(m => m.CidadeId).IsRequired(false);
            entityBuilder.HasMany(m => m.Avaliacoes).WithOne(avaliacao => avaliacao.Aluno).HasForeignKey(avaliacao => avaliacao.AlunoId);
        }
    }
}
