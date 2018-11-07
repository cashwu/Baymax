using Baymax.Entity.Interface;

namespace Baymax.Tests.Entity
{
    public interface ITestUnitOfWork : IBaymaxUnitOfWork<TestDbContext>
    {
    }
}