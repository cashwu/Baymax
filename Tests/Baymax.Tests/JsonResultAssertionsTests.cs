using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Sdk;

namespace Baymax.Tests
{
    public class JsonResultAssertionsTests : ResultAssertionTestBase
    {
        [Fact]
        public void Result_with_data_assertWithData_should_not_throw_exception()
        {
            var jsonResult = new JsonResult(new TestModel {Id = 1});

            var assertions = GivenJsonResultAssertions(jsonResult);

            assertions = assertions.WithData(new TestModel {Id = 1});

            assertions.Should().NotBeNull();
        }
        
        [Fact]
        public void Result_wrong_data_assertWithData_should_throw_exception()
        {
            var jsonResult = new JsonResult(new TestModel {Id = 0});

            var assertions = GivenJsonResultAssertions(jsonResult);
            
            Assert.Throws<ComparisonException>(() => assertions.WithData(new TestModel {Id = 1}));
        }
        
        [Fact]
        public void Result_with_anonymous_data_assertWithAnonymousData_should_not_throw_exception()
        {
            var jsonResult = new JsonResult(new {Id = 1});

            var assertions = GivenJsonResultAssertions(jsonResult);

            assertions = assertions.WithAnonymousData(new {Id = 1});

            assertions.Should().NotBeNull();
        }
        
        [Fact]
        public void Result_wrong_anonymous_data_assertWithAnonymousData_should_throw_exception()
        {
            var jsonResult = new JsonResult(new {Id = 0});

            var assertions = GivenJsonResultAssertions(jsonResult);

            Assert.Throws<XunitException>(() => assertions.WithAnonymousData(new {Id = 1}));
        }

        private JsonResultAssertions GivenJsonResultAssertions(JsonResult jsonResult)
        {
            return new JsonResultAssertions(jsonResult);
        }
    }
}