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
        protected TDbContext _DbContext;
        private Dictionary<Type, object> _repositories;
        private bool _disposed;

        protected BaymaxUnitOfWork(TDbContext context)
        {
            _DbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual TDbContext DbContext
        {
            get => _DbContext;
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
                _repositories[type] = new BaymaxRepository<TEntity>(_DbContext);
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
                _repositories[type] = new BaymaxQueryRepository<TEntity>(_DbContext);
            }

            return (IBaymaxQueryRepository<TEntity>) _repositories[type];
        }

        public virtual int Commit()
        {
            ValidateObject();

            return _DbContext.SaveChanges();
        }

        public virtual Task<int> CommitAsync()
        {
            return _DbContext.SaveChangesAsync();
        }

        public virtual int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _DbContext.Database.ExecuteSqlCommand(sql, parameters);
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

        private IEnumerable<object> GetEntitiesByState(Func<EntityEntry, bool> predicate)
        {
            return _DbContext.ChangeTracker.Entries()
                            .Where(predicate)
                            .Select(a => a.Entity);
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
                _DbContext.Dispose();
            }

            _disposed = true;
        }
    }
}