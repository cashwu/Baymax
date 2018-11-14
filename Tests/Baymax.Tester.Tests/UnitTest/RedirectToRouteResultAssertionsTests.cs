using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class RedirectToRouteResultAssertionsTests
    {
        [Fact]
        public void WithRouteName()
        {
            new RedirectToRouteResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBeRedirectToRouteResult()
                    .WithRouteName("routeName")
                    .WithPermanent(false)
                    .WithPreserveMethod(false);
        }

        [Fact]
        public void WithRouteValue()
        {
            new RedirectToRouteResultController()
                    .AsTester()
                    .Action(c => c.Action2())
                    .ShouldBeRedirectToRouteResult()
                    .WithRouteValue("id", 1);
        }

        [Fact]
        public void WithRouteName_RouteValue()
        {
            new RedirectToRouteResultController()
                    .AsTester()
                    .Action(c => c.Action3())
                    .ShouldBeRedirectToRouteResult()
                    .WithRouteName("routeName")
                    .WithRouteValue("id", 1);
        }

        [Fact]
        public void WithRouteName_RouteValue_Fragment()
        {
            new RedirectToRouteResultController()
                    .AsTester()
                    .Action(c => c.Action4())
                    .ShouldBeRedirectToRouteResult()
                    .WithRouteName("routeName")
                    .WithRouteValue("id", 1)
                    .WithFragment("fragment");
        }

        [Fact]
        public void WithRouteName_Fragment()
        {
            new RedirectToRouteResultController()
                    .AsTester()
                    .Action(c => c.Action5())
                    .ShouldBeRedirectToRouteResult()
                    .WithRouteName("routeName")
                    .WithFragment("fragment");
        }

        [Fact]
        public void WithRouteName_Permanent()
        {
            new RedirectToRouteResultController()
                    .AsTester()
                    .Action(c => c.Action6())
                    .ShouldBeRedirectToRouteResult()
                    .WithRouteName("routeName")
                    .WithPermanent(true);
        }

        [Fact]
        public void WithRouteName_PreserveMethod()
        {
            new RedirectToRouteResultController()
                    .AsTester()
                    .Action(c => c.Action7())
                    .ShouldBeRedirectToRouteResult()
                    .WithRouteName("routeName")
                    .WithPreserveMethod(true);
        }
        
        [Fact]
        public void WithRouteName_Permanent_PreserveMethod()
        {
            new RedirectToRouteResultController()
                    .AsTester()
                    .Action(c => c.Action8())
                    .ShouldBeRedirectToRouteResult()
                    .WithRouteName("routeName")
                    .WithPermanent(true)
                    .WithPreserveMethod(true);
        }
    }

    public class RedirectToRouteResultController : Controller
    {
        public IActionResult Action()
        {
            return RedirectToRoute("routeName");
        }

        public IActionResult Action2()
        {
            return RedirectToRoute(new { id = 1 });
        }

        public IActionResult Action3()
        {
            return RedirectToRoute("routeName", new { id = 1 });
        }

        public IActionResult Action4()
        {
            return RedirectToRoute("routeName", new { id = 1 }, "fragment");
        }

        public IActionResult Action5()
        {
            return RedirectToRoute("routeName", "fragment");
        }

        public IActionResult Action6()
        {
            return RedirectToRoutePermanent("routeName");
        }

        public IActionResult Action7()
        {
            return RedirectToRoutePreserveMethod("routeName");
        }
        
        public IActionResult Action8()
        {
            return RedirectToRoutePermanentPreserveMethod("routeName");
        }
    }
}