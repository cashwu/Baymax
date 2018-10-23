using System;
using System.Collections.Generic;
using Baymax.Extension;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Baymax.Tests.Services
{
    public class ServiceRegisterTests
    {
        [Fact]
        public void DefaultRegister()
        {
            var testService = new ServiceCollection()
                              .AddGeneralService("Baymax.Tests")
                              .BuildServiceProvider()
                              .GetRequiredService<ITestService>();

            testService.GetTestMsg().Should().Be("Test");
        }

        [Fact]
        public void LifeScopeRegister()
        {
            var typeLifetimeDic = new Dictionary<Type, ServiceLifetime>
            {
                { typeof(TestService), ServiceLifetime.Singleton }
            };

            var testService = new ServiceCollection()
                              .AddGeneralService("Baymax.Tests", typeLifetimeDic)
                              .BuildServiceProvider()
                              .GetRequiredService<ITestService>();

            testService.GetTestMsg().Should().Be("Test");
        }

        [Fact]
        public void ArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                  {
                      new ServiceCollection()
                              .AddGeneralService(string.Empty);
                  })
                  .Message
                  .Should()
                  .Contain("prefixAssemblyName");
        }
    }

    public class TestService : ITestService
    {
        public string GetTestMsg()
        {
            return "Test";
        }
    }

    public interface ITestService
    {
        string GetTestMsg();
    }
}