using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Baymax.Entity.Interface;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Entity
{
    public class ViewRepository<TEntity> : IViewRepository<TEntity> where TEntity : ViewEntity
    {
        private readonly DbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        public ViewRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbContext.Query<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.Select(selector);
        }
    }
}