using Baymax.Entity;

namespace Baymax.Tests.Entity
{
    public class TestUnitOfWork : BaymaxUnitOfWork<TestDbContext>, ITestUnitOfWork
    {
        public TestUnitOfWork(TestDbContext context)
                : base(context)
        {
        }
    }
}