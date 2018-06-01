using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class AcceptedResultAssertions<TController> where TController : Controller
    {
        private readonly AcceptedResult _acceptedResult;
        private readonly TController _controller;

        public AcceptedResultAssertions(AcceptedResult acceptedResult, TController controller)
        {
            _acceptedResult = acceptedResult;
            _controller = controller;
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