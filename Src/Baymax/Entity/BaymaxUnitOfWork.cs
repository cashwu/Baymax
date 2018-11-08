using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Baymax.Entity.Interface;
using Baymax.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Baymax.Entity
{
    public class BaymaxUnitOfWork<TDbContext> : IBaymaxUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;
        private Dictionary<Type, object> _repositories;
        private bool _disposed;

        public BaymaxUnitOfWork(TDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual TDbContext DbContext
        {
            get => _context;
        }

        public virtual IBaymaxRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new BaymaxRepository<TEntity>(_context);
            }

            return (IBaymaxRepository<TEntity>) _repositories[type];
        }

        public virtual IBaymaxQueryRepository<TEntity> GetViewRepository<TEntity>() where TEntity : QueryEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new BaymaxQueryRepository<TEntity>(_context);
            }

            return (IBaymaxQueryRepository<TEntity>) _repositories[type];
        }

        public virtual int Commit()
        {
            ValidateObject();

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

        protected IEnumerable<object> GetEntitiesByState(Func<EntityEntry, bool> predicate)
        {
            return DbContext.ChangeTracker.Entries()
                            .Where(predicate)
                            .Select(a => a.Entity);
        }

        private void ValidateObject()
        {
            if (!EntityValidation.AnyProcessRoutines())
            {
                return;
            }

            var entities = GetEntitiesByState(a => a.State == EntityState.Added
                                                   || a.State == EntityState.Modified);

            foreach (var entity in entities)
            {
                var context = new ValidationContext(entity);
                var results = new List<ValidationResult>();
                Validator.TryValidateObject(entity, context, results, true);

                EntityValidation.Check(entity, ref results);

                if (results.Any())
                {
                    throw new EntityValidationException(results);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _repositories?.Clear();
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}