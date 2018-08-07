using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Xunit;
using Xunit.Sdk;

namespace Baymax.Tester.Tests
{
    public class CreatedAtRouteResultAssertionsTests : ResultAssertionTestBase
    {
        [Fact]
        public void Result_with_route_name_assertWithRouteName_should_not_throw_exception()
        {
            var createdAtRouteResult = new CreatedAtRouteResult("name", null, null);

            var assertions = GivenCreatedAtRouteResultAssertions(createdAtRouteResult);

            assertions = assertions.WithRouteName("name");

            assertions.Should().NotBeNull();
        }

        [Fact]
        public void Result_not_route_name_assertWithRouteName_should_throw_exception()
        {
            var createdAtRouteResult = new CreatedAtRouteResult(null, null, null);

            var assertions = GivenCreatedAtRouteResultAssertions(createdAtRouteResult);

            Assert.Throws<XunitException>(() => assertions.WithRouteName("name"));
        }

        [Fact]
        public void Result_with_route_value_assertWithRouteValue_should_not_throw_exception()
        {
            var createdAtRouteResult =
                new CreatedAtRouteResult(null, new RouteValueDictionary {{"key", new TestModel {Id = 1}}}, null);

            var assertions = GivenCreatedAtRouteResultAssertions(createdAtRouteResult);

            assertions = assertions.WithRouteValue("key", new TestModel {Id = 1});

            assertions.Should().NotBeNull();
        }

        [Fact]
        public void Result_wrong_route_value_assertWithRouteValue_should_throw_exception()
        {
            var createdAtRouteResult =
                new CreatedAtRouteResult(null, new RouteValueDictionary {{"key", new TestModel {Id = 0}}}, null);

            var assertions = GivenCreatedAtRouteResultAssertions(createdAtRouteResult);

            Assert.Throws<ComparisonException>(() => assertions.WithRouteValue("key", new TestModel {Id = 1}));
        }

        [Fact]
        public void Result_with_value_assertWithValue_should_not_throw_exception()
        {
            var createdAtRouteResult = new CreatedAtRouteResult(null, null, new TestModel {Id = 1});

            var assertions = GivenCreatedAtRouteResultAssertions(createdAtRouteResult);

            assertions = assertions.WithValue(new TestModel {Id = 1});

            assertions.Should().NotBeNull();
        }
        
        [Fact]
        public void Result_not_value_assertWithValue_should_throw_exception()
        {
            var createdAtRouteResult = new CreatedAtRouteResult(null, null, null);

            var assertions = GivenCreatedAtRouteResultAssertions(createdAtRouteResult);

            Assert.Throws<ComparisonException>(() => assertions.WithValue(new TestModel {Id = 1}));
        }
        
        private CreatedAtRouteResultAssertions GivenCreatedAtRouteResultAssertions(
            CreatedAtRouteResult createdAtRouteResult)
        {
            return new CreatedAtRouteResultAssertions(createdAtRouteResult);
        }
    }
}