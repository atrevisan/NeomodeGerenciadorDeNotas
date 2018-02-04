
using System.Collections.Generic;

namespace GerenciadorDeNotas.Entidades
{
    public class Cidade : IEntidadeBase
    {

        public int ID { get; set; }

        public string Nome { get; set; }

        public string UF { get; set; }

        public virtual ICollection<Aluno> Alunos { get; set; }
    }
}
