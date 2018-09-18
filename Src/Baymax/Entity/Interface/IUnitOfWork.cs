using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Entity.Interface
{
    public interface IUnitOfWork<out TDbContext> : IDisposable where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
        
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        IViewRepository<TEntity> GetViewRepository<TEntity>() where TEntity : ViewEntity;

        int Commit();
        
        Task<int> CommitAsync();
        
        int ExecuteSqlCommand(string sql, params object[] parameters);

        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity;
    }
}