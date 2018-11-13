using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class RedirectToRouteResultAssertions<TController> where TController : Controller
    {
        private readonly RedirectToRouteResult _redirectToRouteResult;

        public RedirectToRouteResultAssertions(RedirectToRouteResult redirectToRouteResult)
        {
            _redirectToRouteResult = redirectToRouteResult;
        }
        
        public RedirectToRouteResultAssertions<TController> WithFragment(string expectedFragment)
        {
            _redirectToRouteResult.Fragment.Should().Be(expectedFragment);

            return this;
        }

        public RedirectToRouteResultAssertions<TController> WithRouteName(string expectedRouteName)
        {
            _redirectToRouteResult.RouteName.Should().Be(expectedRouteName);

            return this;
        }

        public RedirectToRouteResultAssertions<TController> WithRouteValue(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().Should().Be(_redirectToRouteResult.RouteValues[key]);
            
            return this;
        }

        public RedirectToRouteResultAssertions<TController> WithPermanent(bool expectedPermanent)
        {
            _redirectToRouteResult.Permanent.Should().Be(expectedPermanent);

            return this;
        }

        public RedirectToRouteResultAssertions<TController> WithPreserveMethod(bool expectedPreserveMethod)
        {
            _redirectToRouteResult.PreserveMethod.Should().Be(expectedPreserveMethod);

            return this;
        }
    }
}
