using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class ContentResultAssertionsTests
    {
        [Fact]
        public void WithContentAndContentType()
        {
            new ContentResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBeContentResult()
                    .WithContent("abc")
                    .WithContentType("text/xml");
        }
    }

    internal class ContentResultController : Controller
    {
        public IActionResult Action()
        {
            return Content("abc", "text/xml");
        }
    }
}