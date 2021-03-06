using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Baymax.Entity.Interface;
using Baymax.Extension.Entity;
using Baymax.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Baymax.Entity
{
    public class BaymaxRepository<TEntity> : IBaymaxRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        public BaymaxRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
                                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                        int pageIndex = Utilitites.DefaultPageIndex,
                                                        int pageSize = Utilitites.DefaultPageSize,
                                                        bool disableTracking = true)
        {
            var query = GetBaseQuery(predicate, orderBy, include, disableTracking);

            return query.ToPagedList(pageIndex, pageSize);
        }

        public virtual Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                                   int pageIndex = Utilitites.DefaultPageIndex,
                                                                   int pageSize = Utilitites.DefaultPageSize,
                                                                   bool disableTracking = true,
                                                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = GetBaseQuery(predicate, orderBy, include, disableTracking);

            return query.ToPagedListAsync(pageIndex, pageSize, cancellationToken: cancellationToken);
        }

        public virtual IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                                 Expression<Func<TEntity, bool>> predicate = null,
                                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                                 int pageIndex = Utilitites.DefaultPageIndex,
                                                                 int pageSize = Utilitites.DefaultPageSize,
                                                                 bool disableTracking = true) where TResult : class
        {
            var query = GetBaseQuery(predicate, orderBy, include, disableTracking);

            return query.Select(selector).ToPagedList(pageIndex, pageSize);
        }

        public virtual Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                                            Expression<Func<TEntity, bool>> predicate = null,
                                                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                                            int pageIndex = Utilitites.DefaultPageIndex,
                                                                            int pageSize = Utilitites.DefaultPageSize,
                                                                            bool disableTracking = true,
                                                                            CancellationToken cancellationToken = default(CancellationToken)) where TResult : class
        {
            var query = GetBaseQuery(predicate, orderBy, include, disableTracking);

            return query.Select(selector).ToPagedListAsync(pageIndex, pageSize, cancellationToken: cancellationToken);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                          bool disableTracking = true)
        {
            return GetBaseQuery(predicate, orderBy, include, disableTracking);
        }

        public IQueryable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                   Expression<Func<TEntity, bool>> predicate = null,
                                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                   bool disableTracking = true)
        {
            return GetBaseQuery(predicate, orderBy, include, disableTracking).Select(selector);
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                 bool disableTracking = true)
        {
            var query = GetBaseQuery(predicate, orderBy, include, disableTracking);

            return query.FirstOrDefault();
        }

        public virtual TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                          Expression<Func<TEntity, bool>> predicate = null,
                                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                          bool disableTracking = true)
        {
            var query = GetBaseQuery(predicate, orderBy, include, disableTracking);

            return query.Select(selector).FirstOrDefault();
        }

        public virtual Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                                     Expression<Func<TEntity, bool>> predicate = null,
                                                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                                     bool disableTracking = true)
        {
            var query = GetBaseQuery(predicate, orderBy, include, disableTracking);

            return query.Select(selector).FirstOrDefaultAsync();
        }

        public virtual Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                            bool disableTracking = true)
        {
            var query = GetBaseQuery(predicate, orderBy, include, disableTracking);

            return query.FirstOrDefaultAsync();
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual Task<TEntity> FindAsync(params object[] keyValues)
        {
            return _dbSet.FindAsync(keyValues);
        }

        public virtual Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return _dbSet.FindAsync(keyValues, cancellationToken);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? _dbSet.Count() : _dbSet.Count(predicate);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? _dbSet.Any() : _dbSet.Any(predicate);
        }

        public virtual IQueryable<TEntity> FromSql(RawSqlString sql, params object[] parameters)
        {
            return _dbSet.FromSql(sql, parameters);
        }

        public IQueryable<TEntity> FromSql(FormattableString sql)
        {
            return _dbSet.FromSql(sql);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Insert(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual Task InsertAsync(params TEntity[] entities)
        {
            return _dbSet.AddRangeAsync(entities);
        }

        public virtual Task InsertAsync(IEnumerable<TEntity> entities,
                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Update(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(params object[] keyValues)
        {
            var entity = _dbSet.Find(keyValues);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        private IQueryable<TEntity> GetBaseQuery(Expression<Func<TEntity, bool>> predicate,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                                 bool disableTracking)
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
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