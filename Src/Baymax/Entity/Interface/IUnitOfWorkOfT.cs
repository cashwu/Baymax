using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Entity.Interface
{
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext DbContext { get; }
        
        Task<int> CommitAsync(params IUnitOfWork[] unitOfWorks);
    }
}