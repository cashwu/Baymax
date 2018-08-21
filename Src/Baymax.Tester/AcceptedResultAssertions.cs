using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester
{
    public class AcceptedResultAssertions<TController> where TController : Controller
    {
        private readonly AcceptedResult _acceptedResult;

        public AcceptedResultAssertions(AcceptedResult acceptedResult)
        {
            _acceptedResult = acceptedResult;
        }

        public AcceptedResultAssertions<TController> WithLocation(string expectedLocation)
        {
            _acceptedResult.Location.Should().Be(expectedLocation);

            return this;
        }

        public AcceptedResultAssertions<TController> WithValue(object expectValue)
        {
            expectValue.ToExpectedObject().ShouldEqual(_acceptedResult.Value);

            return this;
        }
    }
}
