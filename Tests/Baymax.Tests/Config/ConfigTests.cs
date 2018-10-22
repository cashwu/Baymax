using System.Collections.Generic;
using System.IO;
using Baymax.Extension;
using ExpectedObjects;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Baymax.Tests.Config
{
    public class ConfigTests
    {
        [Fact]
        public void SingleConfig()
        {
            GivenServiceProvider()
                    .GetRequiredService<SingleConfig>()
                    .Id
                    .Should()
                    .Be(123);
        }

        [Fact]
        public void ArrayConfigStruct()
        {
            var config = GivenServiceProvider()
                    .GetRequiredService<List<int>>();

            new List<int> { 1, 2, 3 }.ToExpectedObject().ShouldEqual(config);
        }

        [Fact]
        public void ArrayConfigObject()
        {
            var config = GivenServiceProvider()
                    .GetRequiredService<List<ArrayConfigObject>>();

            new List<ArrayConfigObject>
                    {
                        new ArrayConfigObject
                        {
                            Id = 1,
                            Name = "AA"
                        },
                        new ArrayConfigObject
                        {
                            Id = 2,
                            Name = "BB"
                        },
                        new ArrayConfigObject
                        {
                            Id = 3,
                            Name = "CC"
                        }
                    }.ToExpectedObject().ShouldEqual(config);
        }

        private IConfiguration GivenConfiguration()
        {
            return new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("profile.json")
                   .Build();
        }

        private ServiceProvider GivenServiceProvider()
        {
            return new ServiceCollection()
                   .AddConfig(GivenConfiguration(), "Baymax.Tests")
                   .BuildServiceProvider();
        }
    }
}