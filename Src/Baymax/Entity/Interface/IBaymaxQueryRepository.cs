using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Entity.Interface
{
    public interface IBaymaxQueryRepository<TEntity> where TEntity : QueryEntity
    {
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null);

        IQueryable<TEntity> FromSql(RawSqlString sql, params object[] parameters);
        
        IQueryable<TEntity> FromSql(FormattableString sql);
    }
}