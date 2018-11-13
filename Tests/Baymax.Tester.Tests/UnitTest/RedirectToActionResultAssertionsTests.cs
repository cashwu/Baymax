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
                    .WithAction("actionName")
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
                    .WithAction("actionName")
                    .WithController("controllerName");
        }

        [Fact]
        public void WithAction_RouteValue()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action3())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("actionName")
                    .WithRouteValue("id", 1);
        }
        
        [Fact]
        public void WithAction_Controller_RouteValue()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action4())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("actionName")
                    .WithController("controllerName")
                    .WithRouteValue("id", 1);
        }
        
        [Fact]
        public void WithAction_Controller_RouteValue_Fragment()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action5())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("actionName")
                    .WithController("controllerName")
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
                    .WithAction("actionName")
                    .WithController("controllerName")
                    .WithFragment("fragment");
        }
        
        [Fact]
        public void WithAction_Permanent()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action7())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("actionName")
                    .WithPermanent(true);
        }
        [Fact]
        public void WithAction_PreserveMethod()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action8())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("actionName")
                    .WithPreserveMethod(true);
        }
        
        [Fact]
        public void WithAction_Permanent_PreserveMethod()
        {
            new RedirectToActionResultController()
                    .AsTester()
                    .Action(c => c.Action9())
                    .ShouldBeRedirectToActionResult()
                    .WithAction("actionName")
                    .WithPermanent(true)
                    .WithPreserveMethod(true);
        }
    }

    internal class RedirectToActionResultController : Controller
    {
        public IActionResult Action()
        {
            return RedirectToAction("actionName");
        }

        public IActionResult Action2()
        {
            return RedirectToAction("actionName", "controllerName");
        }

        public IActionResult Action3()
        {
            return RedirectToAction("actionName", new { Id = 1 });
        }
        
        public IActionResult Action4()
        {
            return RedirectToAction("actionName", "controllerName", new { Id = 1 });
        }
        
        public IActionResult Action5()
        {
            return RedirectToAction("actionName", "controllerName", new { Id = 1 }, "fragment");
        }
        
        public IActionResult Action6()
        {
            return RedirectToAction("actionName", "controllerName", "fragment");
        }

        public IActionResult Action7()
        {
            return RedirectToActionPermanent("actionName");
        }
        
        public IActionResult Action8()
        {
            return RedirectToActionPreserveMethod("actionName");
        }
        
        public IActionResult Action9()
        {
            return RedirectToActionPermanentPreserveMethod("actionName");
        }
    }
}