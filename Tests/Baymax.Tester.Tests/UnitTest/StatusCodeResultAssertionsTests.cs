using System.Net;
using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class StatusCodeResultAssertionsTests
    {
        [Fact]
        public void WithStatusCode()
        {
            new StatusCodeResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBeStatusCodeResult()
                    .WithStatusCode(200);
        }
    }

    public class StatusCodeResultController : Controller
    {
        public IActionResult Action()
        {
            return StatusCode((int) HttpStatusCode.OK);
        }
    }
}