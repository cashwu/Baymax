using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Baymax.Entity.Interface;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Entity
{
    public class BaymaxUnitOfWork<TDbContext> : IBaymaxUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private Dictionary<Type, object> repositories;
        private bool disposed;

        public BaymaxUnitOfWork(TDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual TDbContext DbContext
        {
            get
            {
                return _context;
            }
        }

        public virtual IBaymaxRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new BaymaxRepository<TEntity>(_context);
            }

            return (IBaymaxRepository<TEntity>) repositories[type];
        }

        public virtual IBaymaxViewRepository<TEntity> GetViewRepository<TEntity>() where TEntity : ViewEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new BaymaxViewRepository<TEntity>(_context);
            }

            return (IBaymaxViewRepository<TEntity>) repositories[type];
        }

        public virtual int Commit()
        {
            return _context.SaveChanges();
        }

        public virtual Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public virtual int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public virtual IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity
        {
            return _context.Set<TEntity>().FromSql(sql, parameters);
        }

        public virtual void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    repositories?.Clear();
                    _context.Dispose();
                }
            }

            disposed = true;
        }
    }
}