using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Xunit;
using Xunit.Sdk;

namespace Baymax.Tester.Tests
{
    public class CreatedAtActionResultAssertionsTests : ResultAssertionTestBase
    {
        [Fact]
        public void Result_with_action_name_assertWithActionName_should_not_throw_exception()
        {
            var createdAtActionResult = new CreatedAtActionResult("action", null, null, null);

            var assertions = GivenCreatedAtActionResultAssertions(createdAtActionResult);

            assertions = assertions.WithActionName("action");

            assertions.Should().NotBeNull();
        }

        [Fact]
        public void Result_not_action_name_assertWithActionName_should_throw_exception()
        {
            var createdAtActionResult = new CreatedAtActionResult(null, null, null, null);

            var assertions = GivenCreatedAtActionResultAssertions(createdAtActionResult);

            Assert.Throws<XunitException>(() => assertions.WithActionName("action"));
        }

        [Fact]
        public void Result_with_controller_name_assertWithControllerName_should_not_throw_exception()
        {
            var createdAtActionResult = new CreatedAtActionResult(null, "controller", null, null);

            var assertions = GivenCreatedAtActionResultAssertions(createdAtActionResult);

            assertions = assertions.WithControllerName("controller");

            assertions.Should().NotBeNull();
        }

        [Fact]
        public void Result_not_controller_name_assertWithControllerName_should_throw_exception()
        {
            var createdAtActionResult = new CreatedAtActionResult(null, null, null, null);

            var assertions = GivenCreatedAtActionResultAssertions(createdAtActionResult);

            Assert.Throws<XunitException>(() => assertions.WithControllerName("controller"));
        }

        [Fact]
        public void Result_with_route_value_assertWithRouteValue_should_not_throw_exception()
        {
            var createdAtActionResult =
                new CreatedAtActionResult(null, null, new RouteValueDictionary {{"key", new TestModel {Id = 1}}}, null);

            var assertions = GivenCreatedAtActionResultAssertions(createdAtActionResult);

            assertions = assertions.WithRouteValue("key", new TestModel {Id = 1});

            assertions.Should().NotBeNull();
        }

        [Fact]
        public void Result_have_wrong_route_value_assertWithRouteValue_should_throw_exception()
        {
            var createdAtActionResult =
                new CreatedAtActionResult(null, null, new RouteValueDictionary {{"key", new TestModel {Id = 0}}}, null);

            var assertions = GivenCreatedAtActionResultAssertions(createdAtActionResult);

            Assert.Throws<ComparisonException>(() => assertions.WithRouteValue("key", new TestModel {Id = 1}));
        }

        [Fact]
        public void Result_with_value_assertWithValue_should_not_throw_exception()
        {
            var createdAtActionResult =
                new CreatedAtActionResult(null, null, null, new TestModel {Id = 1});

            var assertions = GivenCreatedAtActionResultAssertions(createdAtActionResult);

            assertions = assertions.WithValue(new TestModel {Id = 1});

            assertions.Should().NotBeNull();
        }

        [Fact]
        public void Result_not_value_assertWithValue_should_throw_exception()
        {
            var createdAtActionResult =
                new CreatedAtActionResult(null, null, null, null);

            var assertions = GivenCreatedAtActionResultAssertions(createdAtActionResult);

            Assert.Throws<ComparisonException>(() => assertions.WithValue(new TestModel {Name = "Test"}));
        }

        private CreatedAtActionResultAssertions GivenCreatedAtActionResultAssertions(
            CreatedAtActionResult createdAtActionResult)
        {
            return new CreatedAtActionResultAssertions(createdAtActionResult);
        }
    }
}