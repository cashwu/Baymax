using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class JsonResultAssertionsTests
    {
        [Fact]
        public void WithAnonymousData()
        {
            new JsonResultController()
                    .AsTester()
                    .Action(c => c.JsonResultWithAnonymousData())
                    .ShouldBeJsonResult()
                    .WithAnonymousData(new { Id = 1 });
        }

        [Fact]
        public void WithData()
        {
            new JsonResultController()
                    .AsTester()
                    .Action(c => c.JsonResultWithData())
                    .ShouldBeJsonResult()
                    .WithData(new Data { Id = 1 });
        }
    }

    public class JsonResultController : Controller
    {
        public IActionResult JsonResultWithAnonymousData()
        {
            return new JsonResult(new { Id = 1 });
        }

        public IActionResult JsonResultWithData()
        {
            return new JsonResult(new Data { Id = 1 });
        }
    }

    class Data
    {
        public int Id { get; set; }
    }
}