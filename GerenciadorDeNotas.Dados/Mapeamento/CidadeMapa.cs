using GerenciadorDeNotas.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeNotas.Dados.Mapeamento
{
    public class CidadeMapa : EntidadeBaseMapa<Cidade>
    {
        public CidadeMapa(EntityTypeBuilder<Cidade> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.Property(m => m.Nome).IsRequired();
            entityBuilder.Property(m => m.UF).IsRequired();
        }
    }
}
