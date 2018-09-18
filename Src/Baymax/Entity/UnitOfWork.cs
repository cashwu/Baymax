using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Baymax.Entity.Interface;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Entity
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private Dictionary<Type, object> repositories;
        private bool disposed;

        public UnitOfWork(TDbContext context)
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

        public virtual IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>) repositories[type];
        }

        public virtual IViewRepository<TEntity> GetViewRepository<TEntity>() where TEntity : ViewEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new ViewRepository<TEntity>(_context);
            }

            return (IViewRepository<TEntity>) repositories[type];
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