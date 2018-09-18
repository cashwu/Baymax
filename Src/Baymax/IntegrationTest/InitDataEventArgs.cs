using System;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Tester.Integration
{
    public class InitDataEventArgs<TDbContext> : EventArgs 
            where TDbContext : DbContext
    {
        public InitDataEventArgs(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
        
        public TDbContext DbContext { get; } 
    }
}