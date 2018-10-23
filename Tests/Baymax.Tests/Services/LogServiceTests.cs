using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Baymax.Exception;
using Baymax.Extension;
using Baymax.Services.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Baymax.Tests.Services
{
    public class LogServiceTests
    {
        [Fact]
        public void DefaultLogRegister()
        {
            GivenRequiredService("Test").Log("abc");
        }

        [Fact]
        public void NormalExceptionLogRegister()
        {
            GivenRequiredService("Test").Log(new ArgumentException("Test Argument Exception"));
        }

        [Fact]
        public void EntityValidationExceptionLogRegister()
        {
            var validationResults = new List<ValidationResult>
            {
                new ValidationResult("Name not empty", new List<string> { "Name" }),
                new ValidationResult("Id not empty", new List<string> { "Id" })
            };

            var entityValidationException = new EntityValidationException(validationResults);

            GivenRequiredService("Test2").Log(entityValidationException);
        }

        private ILogService GivenRequiredService(string environmentName)
        {
            return new ServiceCollection()
                   .AddLogService("Baymax.Tests")
                   .AddSingleton<IHostingEnvironment>(new HostingEnvironment { EnvironmentName = environmentName })
                   .BuildServiceProvider()
                   .GetRequiredService<ILogService>();
        }
    }

    public class TestLog : ILogBase
    {
        public Task LogAsync(System.Exception ex, string env)
        {
            if (string.Equals(env, "Test"))
            {
                ex.Should().BeOfType<ArgumentException>();
                ex.As<ArgumentException>().Message.Should().Be("Test Argument Exception");
                return Task.CompletedTask;
            }

            ex.Should().BeOfType<ValidationException>();
            ex.As<ValidationException>().Message.Contains("not empty").Should().BeTrue();

            return Task.CompletedTask;
        }

        public Task LogAsync(string msg, string env)
        {
            msg.Should().Be("abc");
            env.Should().Be("Test");
            return Task.CompletedTask;
        }
    }

    public class TestLog2 : ILogBase
    {
        public Task LogAsync(System.Exception ex, string env)
        {
            if (string.Equals(env, "Test"))
            {
                ex.Should().BeOfType<ArgumentException>();
                ex.As<ArgumentException>().Message.Should().Be("Test Argument Exception");
                return Task.CompletedTask;
            }

            ex.Should().BeOfType<ValidationException>();
            ex.As<ValidationException>().Message.Contains("not empty").Should().BeTrue();

            return Task.CompletedTask;
        }

        public Task LogAsync(string msg, string env)
        {
            msg.Should().Be("abc");
            env.Should().Be("Test");
            return Task.CompletedTask;
        }
    }
}