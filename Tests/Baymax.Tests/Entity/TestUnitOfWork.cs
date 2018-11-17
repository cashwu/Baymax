using Baymax.Entity;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Tests.Entity
{
    public class TestUnitOfWork : BaymaxUnitOfWork<TestDbContext>, ITestUnitOfWork
    {
        private readonly DbContextOptions<TestDbContext> _options;

        public TestUnitOfWork(TestDbContext context, DbContextOptions<TestDbContext> options)
                : base(context)
        {
            _options = options;
        }

        public override TestDbContext DbContext
        {
            get => _DbContext ?? (_DbContext = new TestDbContext(_options));
        }
    }
}