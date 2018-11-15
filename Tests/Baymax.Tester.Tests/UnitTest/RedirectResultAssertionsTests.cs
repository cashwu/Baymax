using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class RedirectResultAssertionsTests
    {
        [Fact]
        public void WithUrl()
        {
            new RedirectResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBeRedirectResult()
                    .WithUrl("url")
                    .WithPermanent(false)
                    .WithPreserveMethod(false);
        }

        [Fact]
        public void WithUrl_Permanent_true()
        {
            new RedirectResultController()
                    .AsTester()
                    .Action(c => c.Action2())
                    .ShouldBeRedirectResult()
                    .WithUrl("url")
                    .WithPermanent(true)
                    .WithPreserveMethod(false);
        }

        [Fact]
        public void WithUrl_Permanent_PreserveMethod()
        {
            new RedirectResultController()
                    .AsTester()
                    .Action(c => c.Action3())
                    .ShouldBeRedirectResult()
                    .WithUrl("url")
                    .WithPermanent(true)
                    .WithPreserveMethod(true);
        }
    }

    public class RedirectResultController : Controller
    {
        public IActionResult Action()
        {
            return new RedirectResult("url");
        }

        public IActionResult Action2()
        {
            return new RedirectResult("url", true);
        }

        public IActionResult Action3()
        {
            return new RedirectResult("url", true, true);
        }
    }
}