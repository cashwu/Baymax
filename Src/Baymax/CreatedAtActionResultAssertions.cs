using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class CreatedAtActionResultAssertions
    {
        private readonly CreatedAtActionResult _createdAtActionResult;

        public CreatedAtActionResultAssertions(CreatedAtActionResult createdAtActionResult)
        {
            _createdAtActionResult = createdAtActionResult;
        }

        public CreatedAtActionResultAssertions WithActionName(string expectedActionName)
        {
            _createdAtActionResult.ActionName.Should().Be(expectedActionName);

            return this;
        }

        public CreatedAtActionResultAssertions WithControllerName(string expectedControllerName)
        {
            _createdAtActionResult.ControllerName.Should().Be(expectedControllerName);

            return this;
        }

        public CreatedAtActionResultAssertions WithRouteValue(string key, string expectedValue)
        {
            _createdAtActionResult.RouteValues[key].Should().Be(expectedValue);

            return this;
        }

        public CreatedAtActionResultAssertions WithValue(object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_createdAtActionResult.Value);

            return this;
        }
    }
}