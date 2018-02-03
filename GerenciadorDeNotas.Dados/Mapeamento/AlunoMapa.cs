using GerenciadorDeNotas.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeNotas.Dados.Mapeamento
{
    public class AlunoMapa : EntidadeBaseMapa<Aluno>
    {
        public AlunoMapa(EntityTypeBuilder<Aluno> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.Property(m => m.NomeCompleto).IsRequired().HasMaxLength(50);
            entityBuilder.Property(m => m.Matricula).IsRequired();
            entityBuilder.Property(m => m.Foto).IsRequired().HasMaxLength(50);
            entityBuilder.Property(m => m.Telefone).IsRequired().HasMaxLength(16);
            entityBuilder.Property(m => m.EMail).IsRequired().HasMaxLength(50);
            entityBuilder.Property(m => m.CidadeId).IsRequired();
            entityBuilder.HasMany(m => m.Avaliacoes).WithOne(avaliacao => avaliacao.Aluno).HasForeignKey(avaliacao => avaliacao.AlunoId);
        }
    }
}
