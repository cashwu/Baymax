using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class ViewResultAssertionsTests
    {
        [Fact]
        public void WithView()
        {
            new ViewResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBeViewResult()
                    .WithViewName("viewName");
        }

        [Fact]
        public void WithModel()
        {
            new ViewResultController()
                    .AsTester()
                    .Action(c => c.Action2())
                    .ShouldBeViewResult()
                    .WithViewName(null)
                    .WithModel(new { id = 1 });
        }

        [Fact]
        public void WithViewName_Model()
        {
            new ViewResultController()
                    .AsTester()
                    .Action(c => c.Action3())
                    .ShouldBeViewResult()
                    .WithViewName("viewName")
                    .WithModel(new { id = 1 });
        }

        [Fact]
        public void WithViewBag_ViewData()
        {
            new ViewResultController()
                    .AsTester()
                    .Action(c => c.Action4())
                    .ShouldBeViewResult()
                    .WithViewBag("bag", 123)
                    .WithViewData("data", 456)
                    .WithNotTempData();
        }
    }

    public class ViewResultController : Controller
    {
        public IActionResult Action()
        {
            return View("viewName");
        }

        public IActionResult Action2()
        {
            return View(new { id = 1 });
        }

        public IActionResult Action3()
        {
            return View("viewName", new { id = 1 });
        }

        public IActionResult Action4()
        {
            ViewBag.Bag = 123;
            ViewData["data"] = 456;

            return View();
        }
    }
}