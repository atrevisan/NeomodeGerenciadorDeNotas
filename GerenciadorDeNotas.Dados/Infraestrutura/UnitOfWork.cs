namespace GerenciadorDeNotas.Dados.Infraestrutura
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GerenciadorDeNotasContexto _dbContext;

        public UnitOfWork(GerenciadorDeNotasContexto dbContext)
        {
            this._dbContext = dbContext;
        }
        
        public void Commit()
        {
            _dbContext.Commit();
        }
    }
}
