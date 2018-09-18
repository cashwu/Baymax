using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class StatusCodeResultAssertions<TController> where TController : Controller
    {
        private readonly StatusCodeResult _statusCodeResult;

        public StatusCodeResultAssertions(StatusCodeResult statusCodeResult)
        {
            _statusCodeResult = statusCodeResult;
        }

        public StatusCodeResultAssertions<TController> WithStatusCode(int expectedStatudCode)
        {
            _statusCodeResult.StatusCode.Should().Be(expectedStatudCode);
            
            return this;
        }
    }
}
