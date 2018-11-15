using Baymax.Tester.Integration;
using Baymax.Tester.Web;
using Xunit;

namespace Baymax.Tester.Tests.Integration
{
    public class TestBase : IClassFixture<ApplicationFactory<Startup, AppDbContext>>
    {
        protected readonly ApplicationFactory<Startup, AppDbContext> Factory;

        public TestBase(ApplicationFactory<Startup, AppDbContext> factory)
        {
            Factory = factory;
            Factory.InitDataEvent += OnInitDataEvent;
        }

        private void OnInitDataEvent(object sender, InitDataEventArgs<AppDbContext> e)
        {
            e.DbContext.Info.Add(new Info { Id = 1, Name = "Test123" });
            e.DbContext.Info.Add(new Info { Id = 2, Name = "Test456" });
            e.DbContext.SaveChanges();
        }
    }
}