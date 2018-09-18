using System;
using System.Linq;
using System.Threading.Tasks;

namespace Baymax.Entity.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        IViewRepository<TEntity> GetViewRepository<TEntity>() where TEntity : ViewEntity;

        int Commit();
        
        Task<int> CommitAsync();
        
        int ExecuteSqlCommand(string sql, params object[] parameters);

        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity;
    }
}