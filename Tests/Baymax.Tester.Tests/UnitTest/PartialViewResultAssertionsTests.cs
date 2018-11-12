using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class PartialViewResultAssertionsTests
    {
        [Fact]
        public void WithPartialViewName()
        {
            new PartialViewResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBePartialViewResult()
                    .WithNotTempData()
                    .WithViewName("partialViewName");
        }
        
        [Fact]
        public void WithModel()
        {
            new PartialViewResultController()
                    .AsTester()
                    .Action(c => c.Action2())
                    .ShouldBePartialViewResult()
                    .WithNotTempData()
                    .WithDefaultViewName() 
                    .WithModel(new { data = 123 });
        }
        
        [Fact]
        public void WithPartialName_Model()
        {
            new PartialViewResultController()
                    .AsTester()
                    .Action(c => c.Action3())
                    .ShouldBePartialViewResult()
                    .WithNotTempData()
                    .WithViewName("partialViewName")
                    .WithModel(new { data = 123 });
        }
        
        [Fact]
        public void WithPartialName_Model_ViewBag_ViewDataj()
        {
            new PartialViewResultController()
                    .AsTester()
                    .Action(c => c.Action4())
                    .ShouldBePartialViewResult()
                    .WithViewName("partialViewName")
                    .WithModel(new { data = 123 })
                    .WithViewBagOrViewData("Bag", 123)
                    .WithViewBagOrViewData("data", 456);
        }
    }

    internal class PartialViewResultController : Controller
    {
        public IActionResult Action()
        {
            return PartialView("partialViewName");
        }

        public IActionResult Action2()
        {
            return PartialView(new { data = 123 });
        }
        
        public IActionResult Action3()
        {
            return PartialView("partialViewName", new { data = 123 });
        }
        
        public IActionResult Action4()
        {
            ViewBag.Bag = 123;
            ViewData["data"] = 456;
            
            return PartialView("partialViewName", new { data = 123 });
        }
    }
}