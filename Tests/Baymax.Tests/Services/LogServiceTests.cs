using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            GivenRequiredService().Log("abc");

            TestLog.GetMessage().Should().Be("abc");
        }

        [Fact]
        public void NormalExceptionLogRegister()
        {
            GivenRequiredService().Log(new ArgumentException("Test Argument Exception"));

            var ex = TestLog.GetException().First();
            ex.Should().BeOfType<ArgumentException>();
            ex.As<ArgumentException>().Message.Should().Be("Test Argument Exception");
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

            GivenRequiredService().Log(entityValidationException);

            var ex = TestLog.GetException();
            ex[0].Should().BeOfType<ValidationException>();
            ex[0].As<ValidationException>().Message.Should().Be("Name not empty");

            ex[1].Should().BeOfType<ValidationException>();
            ex[1].As<ValidationException>().Message.Should().Be("Id not empty");
        }

        private ILogService GivenRequiredService()
        {
            return new ServiceCollection()
                   .AddLogService("Baymax.Tests")
                   // .AddSingleton<IHostingEnvironment>(new HostingEnvironment { EnvironmentName = environmentName })
                   .BuildServiceProvider()
                   .GetRequiredService<ILogService>();
        }
    }

    public class TestLog : ILogBase
    {
        private static List<System.Exception> exceptions;
        private static string msg;

        public TestLog()
        {
            exceptions = new List<System.Exception>();     
        }
        
        public static List<System.Exception> GetException()
        {
            return exceptions;
        }

        public static string GetMessage()
        {
            return msg;
        }

        public Task LogAsync(System.Exception ex)
        {
            exceptions.Add(ex);

            return Task.CompletedTask;
        }

        public Task LogAsync(string msg)
        {
            TestLog.msg = msg;

            return Task.CompletedTask;
        }
    }
}