namespace GerenciadorDeNotas.Entidades
{
    public class Avaliacao : IEntidadeBase
    {
        public int ID { get; set; }

        public decimal Nota { get; set; }

        public int AlunoId { get; set; }

        public virtual Aluno Aluno { get; set; }


    }
}
