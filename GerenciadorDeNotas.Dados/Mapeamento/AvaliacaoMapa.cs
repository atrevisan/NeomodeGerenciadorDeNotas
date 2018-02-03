using GerenciadorDeNotas.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeNotas.Dados.Mapeamento
{
    public class AvaliacaoMapa :  EntidadeBaseMapa<Avaliacao>
    {
        public AvaliacaoMapa(EntityTypeBuilder<Avaliacao> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.Property(m => m.Nota).IsRequired();
            entityBuilder.Property(m => m.AlunoId).IsRequired();
        }
    }
}
