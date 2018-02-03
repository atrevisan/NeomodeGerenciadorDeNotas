namespace GerenciadorDeNotas.Dados.Infraestrutura
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
