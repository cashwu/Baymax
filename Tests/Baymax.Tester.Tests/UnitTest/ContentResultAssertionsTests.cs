using System.Threading.Tasks;
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
        
        [Fact]
        public void AsyncWithContent()
        {
            new ContentResultController()
                    .AsTester()
                    .Action(c => c.AsyncAction())
                    .ShouldBeContentResult()
                    .WithContent("Test")
                    .WithContentType(null);
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

        public async Task<IActionResult> AsyncAction()
        {
            var result = await Download(); 
            
            return Content(result);
        }

        private async Task<string> Download()
        {
            return await Task.FromResult("Test");
        }
    }
}