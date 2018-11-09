using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Baymax.Extension;
using Baymax.Services;
using Baymax.Services.Interface;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Baymax.Tests.Services
{
    public class BackgroundService
    {
        [Fact]
        public async Task DefaultRegister()
        {
            var serviceProvider = new ServiceCollection()
                                  .AddSingleton(GivenConfiguration())
                                  .AddBackgroundService(typeof(TestBackgroundService))
                                  .BuildServiceProvider();

            var service = serviceProvider.GetService<IHostedService>() as BaymaxBackgroundService<TestBackgroundService>;

            await service.StartAsync(CancellationToken.None);

            TestBackgroundService.GetRun().Should().Be(1);

            await service.StopAsync(CancellationToken.None);

            TestBackgroundService.GetRun().Should().Be(0);

            service.Dispose();
        }

        [Fact]
        public void ParamsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() =>
                  {
                      new ServiceCollection().AddBackgroundService();
                  })
                  .Message.Should()
                  .Contain("type");
        }

        [Fact]
        public void NotImplementType()
        {
            Assert.Throws<ArgumentException>(() =>
                  {
                      new ServiceCollection().AddBackgroundService(typeof(NotImplementType));
                  })
                  .Message.Should()
                  .Be("Not implement type IBackgroundProcessService");
        }

        private IConfiguration GivenConfiguration()
        {
            return new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddInMemoryCollection(new List<KeyValuePair<string, string>>
                   {
                       new KeyValuePair<string, string>($"BackgroundService:{typeof(TestBackgroundService).Name}Interval", "100000")
                   })
                   .Build();
        }
    }

    public class NotImplementType
    {
    }

    public class TestBackgroundService : IBackgroundProcessService
    {
        public TestBackgroundService()
        {
            run = 0;
        }

        private static int run;

        public static int GetRun()
        {
            return run;
        }

        public void DoWork()
        {
            run++;
        }

        public void StopWork()
        {
            run = 0;
        }
    }
}