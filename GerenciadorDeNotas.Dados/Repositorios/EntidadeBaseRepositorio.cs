using GerenciadorDeNotas.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GerenciadorDeNotas.Dados.Repositorios
{
    public class EntidadeBaseRepositorio<T> : IEntidadeBaseRepositorio<T>
              where T : class, IEntidadeBase, new()
    {
        
        
        private readonly GerenciadorDeNotasContexto _contexto;

        public EntidadeBaseRepositorio(GerenciadorDeNotasContexto contexto)
        {
            this._contexto = contexto;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _contexto.Set<T>();
        }
        public virtual IQueryable<T> All
        {
            get
            {

                return GetAll();
            }
        }
        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = _contexto.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);


            }
            return query;
        }
        public T GetSingle(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _contexto.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = _contexto.Entry<T>(entity);
            _contexto.Set<T>().Add(entity);
        }
        public virtual void Edit(T entity)
        {
            EntityEntry dbEntityEntry = _contexto.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _contexto.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }
    }
}
