using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Baymax.Extension;
using Baymax.Services;
using Baymax.Services.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Baymax.Tests.Services
{
    public class BackgroundService
    {
        [Fact]
        public async Task DefaultRegister()
        {
            var service = GivenServiceProvider("Prod").GetService<IHostedService>()
                                  as BaymaxBackgroundService<TestBackgroundService>;

            TestBackgroundService.Init();

            await service.StartAsync(CancellationToken.None);

            TestBackgroundService.GetRun().Should().Be(1);

            await service.StopAsync(CancellationToken.None);

            TestBackgroundService.GetRun().Should().Be(-1);

            service.Dispose();
        }

        [Fact]
        public async Task TestEnv_NotRegister()
        {
            var service = GivenServiceProvider("Test").GetService<IHostedService>()
                                  as BaymaxBackgroundService<TestBackgroundService>;

            TestBackgroundService.Init();

            await service.StartAsync(CancellationToken.None);

            TestBackgroundService.GetRun().Should().Be(0);

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

        private ServiceProvider GivenServiceProvider(string environmentName)
        {
            return new ServiceCollection()
                   .AddSingleton<IHostingEnvironment>(new HostingEnvironment
                   {
                       EnvironmentName = environmentName
                   })
                   .AddSingleton(GivenConfiguration())
                   .AddBackgroundService(typeof(TestBackgroundService))
                   .BuildServiceProvider();
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

        public static void Init()
        {
            run = 0;
        }

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
            run--;
        }
    }
}