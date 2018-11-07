using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Baymax.Tests.Entity
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void Init()
        {
            var unitOfWork = GivenUnitOfWork();

            unitOfWork.DbContext.Should().NotBeNull();
            var repo = unitOfWork.GetRepository<Person>();
            repo.Should().NotBeNull();

            var repo2 = unitOfWork.GetRepository<Person>();
            repo.Should().BeSameAs(repo2);

            var repoView = unitOfWork.GetViewRepository<PersonView>();
            repoView.Should().NotBeNull();

            var repoView2 = unitOfWork.GetViewRepository<PersonView>();
            repoView.Should().BeSameAs(repoView2);
        }

        private ITestUnitOfWork GivenUnitOfWork()
        {
            return new ServiceCollection()
                   .AddDbContext<TestDbContext>(options =>
                   {
                       options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                   })
                   .AddScoped<ITestUnitOfWork, TestUnitOfWork>()
                   .BuildServiceProvider()
                   .GetRequiredService<ITestUnitOfWork>();
        }
    }
}