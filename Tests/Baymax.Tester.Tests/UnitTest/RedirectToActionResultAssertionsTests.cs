using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class RedirectToActionResultAssertionsTests
    {
        [Fact]
        public void WithAction()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("Action")
                    .WithNotTempData()
                    .WithPermanent(false)
                    .WithPreserveMethod(false);
        }

        [Fact]
        public void WithAction_Controller()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action2())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("Action")
                    .WithController("RedirectToActionResult");
        }

        [Fact]
        public void WithAction_RouteValue()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action3())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("Action")
                    .WithRouteValue("id", 1);
        }
        
        [Fact]
        public void WithAction_Controller_RouteValue()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action4())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("Action")
                    .WithController("RedirectToActionResult")
                    .WithRouteValue("id", 1);
        }
        
        [Fact]
        public void WithAction_Controller_RouteValue_Fragment()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action5())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("Action")
                    .WithController("RedirectToActionResult")
                    .WithRouteValue("id", 1)
                    .WithFragment("fragment");
        }
        
        [Fact]
        public void WithAction_Controller_Fragment()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action6())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("Action")
                    .WithController("RedirectToActionResult")
                    .WithFragment("fragment");
        }
        
        [Fact]
        public void WithAction_Permanent()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action7())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("Action")
                    .WithPermanent(true);
        }
        [Fact]
        public void WithAction_PreserveMethod()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action8())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("Action")
                    .WithPreserveMethod(true);
        }
        
        [Fact]
        public void WithAction_Permanent_PreserveMethod()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action9())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("Action")
                    .WithPermanent(true)
                    .WithPreserveMethod(true);
        }
    }

    public class RedirectToActionResultController : Controller
    {
        public IActionResult Action()
        {
            return RedirectToAction("Action");
        }

        public IActionResult Action2()
        {
            return RedirectToAction("Action", "RedirectToActionResult");
        }

        public IActionResult Action3()
        {
            return RedirectToAction("Action", new { Id = 1 });
        }
        
        public IActionResult Action4()
        {
            return RedirectToAction("Action", "RedirectToActionResult", new { Id = 1 });
        }
        
        public IActionResult Action5()
        {
            return RedirectToAction("Action", "RedirectToActionResult", new { Id = 1 }, "fragment");
        }
        
        public IActionResult Action6()
        {
            return RedirectToAction("Action", "RedirectToActionResult", "fragment");
        }

        public IActionResult Action7()
        {
            return RedirectToActionPermanent("Action");
        }
        
        public IActionResult Action8()
        {
            return RedirectToActionPreserveMethod("Action");
        }
        
        public IActionResult Action9()
        {
            return RedirectToActionPermanentPreserveMethod("Action");
        }
    }
}