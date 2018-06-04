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

        public CreatedAtActionResultAssertions WithRouteValue(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldMatch(_createdAtActionResult.RouteValues[key]);

            return this;
        }

        public CreatedAtActionResultAssertions WithValue(object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_createdAtActionResult.Value);

            return this;
        }
    }
}