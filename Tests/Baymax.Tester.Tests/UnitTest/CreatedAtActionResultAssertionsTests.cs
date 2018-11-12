using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class CreatedAtActionResultAssertionsTests
    {
        [Fact]
        public void WithActionName_ControllerName_Value_RouteValue()
        {
            new CreatedAtActionResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBeCreatedAtActionResult()
                    .WithActionName("actionName")
                    .WithControllerName("controllerName")
                    .WithValue(new { data = 123 })
                    .WithRouteValue("Id", 1);
        }

        [Fact]
        public void WithActionName_Value()
        {
            new CreatedAtActionResultController()
                    .AsTester()
                    .Action(c => c.Action1())
                    .ShouldBeCreatedAtActionResult()
                    .WithActionName("actionName")
                    .WithControllerName(null)
                    .WithValue(new { data = 123 });
        }

        [Fact]
        public void WithActionName_Value_RouteValue()
        {
            new CreatedAtActionResultController()
                    .AsTester()
                    .Action(c => c.Action3())
                    .ShouldBeCreatedAtActionResult()
                    .WithActionName("actionName")
                    .WithControllerName(null)
                    .WithRouteValue("id", 1)
                    .WithValue(new { data = 123 });
        }
    }

    internal class CreatedAtActionResultController : Controller
    {
        public IActionResult Action()
        {
            return CreatedAtAction("actionName", "controllerName", new { Id = 1 }, new { data = 123 });
        }

        public IActionResult Action1()
        {
            return CreatedAtAction("actionName", new { data = 123 });
        }

        public IActionResult Action3()
        {
            return CreatedAtAction("actionName", new { Id = 1 }, new { data = 123 });
        }
    }
}