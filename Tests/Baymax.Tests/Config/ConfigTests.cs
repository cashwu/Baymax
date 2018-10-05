using System.IO;
using Baymax.Extension;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Baymax.Tests.Config
{
    public class ConfigTests
    {
        [Fact]
        public void SingleConfig()
        {
            var path = Directory.GetCurrentDirectory();

            IConfiguration configuration = new ConfigurationBuilder()
                                           .SetBasePath(path)
                                           .AddJsonFile("profile.json")
                                           .Build();

            new ServiceCollection()
                    .AddDefaultConfigMapping(configuration, "Baymax.Tests")
                    .BuildServiceProvider()
                    .GetRequiredService<SingleConfig>()
                    .Id
                    .Should()
                    .Be(123);
        }
    }
}