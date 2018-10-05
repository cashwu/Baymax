using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class CreatedAtRouteResultAssertions
    {
        private readonly CreatedAtRouteResult _createdAtRouteResult;

        public CreatedAtRouteResultAssertions(CreatedAtRouteResult createdAtRouteResult)
        {
            _createdAtRouteResult = createdAtRouteResult;
        }

        public CreatedAtRouteResultAssertions WithRouteName(string expectedRouteName)
        {
            _createdAtRouteResult.RouteName.Should().Be(expectedRouteName);
            
            return this;
        }

        public CreatedAtRouteResultAssertions WithRouteValue(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_createdAtRouteResult.RouteValues[key]);
            
            return this;
        }

        public CreatedAtRouteResultAssertions WithValue(object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_createdAtRouteResult.Value);

            return this;
        }
    }
}