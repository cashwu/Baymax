using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Baymax.Entity.Interface;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Entity
{
    public class BaymaxQueryRepository<TEntity> : IBaymaxQueryRepository<TEntity> where TEntity : QueryEntity
    {
        private readonly DbQuery<TEntity> _dbQuery;

        public BaymaxQueryRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            
            _dbQuery = dbContext.Query<TEntity>();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true)
        {
            return GetBaseQuery(predicate, orderBy, disableTracking);
        }

        public IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool disableTracking = true)
        {
            return GetBaseQuery(predicate, orderBy, disableTracking).Select(selector);
        }

        public IQueryable<TEntity> FromSql(RawSqlString sql, params object[] parameters) 
        {
            return _dbQuery.FromSql(sql, parameters);
        }

        public IQueryable<TEntity> FromSql(FormattableString sql)
        {
            return _dbQuery.FromSql(sql);
        }

        private IQueryable<TEntity> GetBaseQuery(Expression<Func<TEntity, bool>> predicate,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                 bool disableTracking)
        {
            IQueryable<TEntity> query = _dbQuery;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}