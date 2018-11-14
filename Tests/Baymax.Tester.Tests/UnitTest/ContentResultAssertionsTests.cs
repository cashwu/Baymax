using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class ContentResultAssertionsTests
    {
        [Fact]
        public void WithContent()
        {
            new ContentResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBeContentResult()
                    .WithContent("abc")
                    .WithContentType(null);
        }
        
        [Fact]
        public void WithContent_ContentType()
        {
            new ContentResultController()
                    .AsTester()
                    .Action(c => c.Action2())
                    .ShouldBeContentResult()
                    .WithContent("abc")
                    .WithContentType("text/xml");
        }
    }

    public class ContentResultController : Controller
    {
        public IActionResult Action()
        {
            return Content("abc");
        }
        
        public IActionResult Action2()
        {
            return Content("abc", "text/xml");
        }
    }
}