using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class AcceptedResultAssertionsTests
    {
        [Fact]
        public void WithLocationAndValue()
        {
            new AcceptedResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldAcceptedResult()
                    .WithLocation("location")
                    .WithValue(new { Id = 1 });
        }
    }

    internal class AcceptedResultController : Controller
    {
        public IActionResult Action()
        {
            return new AcceptedResult("location", new { Id = 1 });
        }
    }
}