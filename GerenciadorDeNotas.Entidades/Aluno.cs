using System;
using System.Collections.Generic;

namespace GerenciadorDeNotas.Entidades
{
    public class Aluno : IEntidadeBase
    {
        public int ID { get; set; }

        public string NomeCompleto { get; set; }

        public string Matricula { get; set; }

        public string Foto { get; set; }

        public string Telefone { get; set; }

        public string EMail { get; set; }

        public int? CidadeId { get; set; }

        public virtual Cidade Cidade { get; set; }

        public virtual ICollection<Avaliacao> Avaliacoes { get; set; }
    }
}
