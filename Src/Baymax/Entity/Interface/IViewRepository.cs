using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Baymax.Entity.Interface
{
    public interface IViewRepository<TEntity>  where TEntity : ViewEntity
    {
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null);
    }
}