using GerenciadorDeNotas.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeNotas.Dados.Mapeamento
{
    public class EntidadeBaseMapa <T> where T : class, IEntidadeBase
    {
        public EntidadeBaseMapa(EntityTypeBuilder<T> entityBuilder)
        {
            entityBuilder.HasKey(t => t.ID);
        }
    }
}
