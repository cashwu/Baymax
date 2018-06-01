using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class CreatedAtRouteResultAssertions<TController> where TController : Controller
    {
        private readonly CreatedAtRouteResult _createdAtRouteResult;
        private readonly TController _controller;

        public CreatedAtRouteResultAssertions(CreatedAtRouteResult createdAtRouteResult, TController controller)
        {
            _createdAtRouteResult = createdAtRouteResult;
            _controller = controller;
        }

        public CreatedAtRouteResultAssertions<TController> WithRouteName(string expectedRouteName)
        {
            _createdAtRouteResult.RouteName.Should().Be(expectedRouteName);
            
            return this;
        }

        public CreatedAtRouteResultAssertions<TController> WithRouteValue(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_createdAtRouteResult.RouteValues[key]);
            
            return this;
        }

        public CreatedAtRouteResultAssertions<TController> WithValue(object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_createdAtRouteResult.Value);

            return this;
        }
    }
}