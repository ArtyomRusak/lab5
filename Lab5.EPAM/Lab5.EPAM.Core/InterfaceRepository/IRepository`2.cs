using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lab5.EPAM.Core.InterfaceRepository
{
    public interface IRepository<TEntity, TKey> : IRepository where TEntity : Entity
    {
        void Create(TEntity value);
        void Update(TEntity value);
        void Remove(TEntity value);
        TEntity GetEntityById(TKey id);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> All();
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
    }
}
