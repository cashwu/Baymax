using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Entity.Interface
{
    public interface IBaymaxUnitOfWork<out TDbContext> : IDisposable where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
        
        IBaymaxRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        IBaymaxViewRepository<TEntity> GetViewRepository<TEntity>() where TEntity : ViewEntity;

        int Commit();
        
        Task<int> CommitAsync();
        
        int ExecuteSqlCommand(string sql, params object[] parameters);

        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity;
    }
}