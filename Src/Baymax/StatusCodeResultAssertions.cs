using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class StatusCodeResultAssertions<TController> where TController : Controller
    {
        private readonly StatusCodeResult _statusCodeResult;
        private readonly TController _controller;

        public StatusCodeResultAssertions(StatusCodeResult statusCodeResult, TController controller)
        {
            _statusCodeResult = statusCodeResult;
            _controller = controller;
        }

        public StatusCodeResultAssertions<TController> WithStatusCode(int expectedStatudCode)
        {
            _statusCodeResult.StatusCode.Should().Be(expectedStatudCode);
            
            return this;
        }
    }
}