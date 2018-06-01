using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class CreatedAtActionResultAssertions<TController> where TController : Controller
    {
        private readonly CreatedAtActionResult _createdAtActionResult;
        private readonly TController _controller;

        public CreatedAtActionResultAssertions(CreatedAtActionResult createdAtActionResult, TController controller)
        {
            _createdAtActionResult = createdAtActionResult;
            _controller = controller;
        }

        public CreatedAtActionResultAssertions<TController> WithActionName(string expectedActionName)
        {
            _createdAtActionResult.ActionName.Should().Be(expectedActionName);

            return this;
        }
        
        public CreatedAtActionResultAssertions<TController> WithControllerName(string expectedControllerName)
        {
            _createdAtActionResult.ControllerName.Should().Be(expectedControllerName);

            return this;
        }
        
        public CreatedAtActionResultAssertions<TController> WithRouteValue(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().Should().Be(_createdAtActionResult.RouteValues[key]);

            return this;
        }
        
        public CreatedAtActionResultAssertions<TController> WithValue(object expectedValue)
        {
            expectedValue.ToExpectedObject().Should().Be(_createdAtActionResult.Value);

            return this;
        }
    }
}