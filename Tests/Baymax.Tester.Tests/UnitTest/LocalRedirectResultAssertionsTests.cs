using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class LocalRedirectResultAssertionsTests
    {
        [Fact]
        public void WithUrl()
        {
            new LocalRedirectResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBeLocalRedirectResult()
                    .WithUrl("localUrl")
                    .WithPermanent(false);
        }
        
        [Fact]
        public void WithUrl_Permanent()
        {
            new LocalRedirectResultController()
                    .AsTester()
                    .Action(c => c.Action2())
                    .ShouldBeLocalRedirectResult()
                    .WithUrl("localUrl")
                    .WithPermanent(true);
        }
        
        [Fact]
        public void WithUrl_PreserveMethod()
        {
            new LocalRedirectResultController()
                    .AsTester()
                    .Action(c => c.Action3())
                    .ShouldBeLocalRedirectResult()
                    .WithUrl("localUrl")
                    .WithPermanent(false)
                    .WithPreserveMethod(true);
        }
        
        [Fact]
        public void WithUrl_Permanent_PreserveMethod()
        {
            new LocalRedirectResultController()
                    .AsTester()
                    .Action(c => c.Action4())
                    .ShouldBeLocalRedirectResult()
                    .WithUrl("localUrl")
                    .WithPermanent(true)
                    .WithPreserveMethod(true);
        }
    }

    public class LocalRedirectResultController : Controller
    {
        public IActionResult Action()
        {
            return LocalRedirect("localUrl");
        }
        
        public IActionResult Action2()
        {
            return LocalRedirectPermanent("localUrl");
        }
        
        public IActionResult Action3()
        {
            return LocalRedirectPreserveMethod("localUrl");
        }
        
        public IActionResult Action4()
        {
            return LocalRedirectPermanentPreserveMethod("localUrl");
        }
    }
}