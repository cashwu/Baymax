using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Baymax.Entity.Interface
{
    public interface IBaymaxRepository<TEntity> where TEntity : BaseEntity
    {
        void ChangeTable(string table);

        IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true);

        Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true, CancellationToken cancellationToken = default(CancellationToken));

        IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true) where TResult : class;

        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true, CancellationToken cancellationToken = default(CancellationToken)) where TResult : class;

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);

        TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);

        Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);

        IQueryable<TEntity> FromSql(RawSqlString sql, params object[] parameters);
        
        IQueryable<TEntity> FromSql(FormattableString sql);

        TEntity Find(params object[] keyValues);

        Task<TEntity> FindAsync(params object[] keyValues);

        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);

        int Count(Expression<Func<TEntity, bool>> predicate = null);

        void Insert(TEntity entity);

        void Insert(params TEntity[] entities);

        void Insert(IEnumerable<TEntity> entities);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task InsertAsync(params TEntity[] entities);

        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        void Update(TEntity entity);

        void Update(params TEntity[] entities);

        void Update(IEnumerable<TEntity> entities);

        void Delete(object id);

        void Delete(TEntity entity);

        void Delete(params TEntity[] entities);

        void Delete(IEnumerable<TEntity> entities);

        IQueryable<TEntity> GetAll();
        
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, bool disableTrack = true);
        
        IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null);

        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}