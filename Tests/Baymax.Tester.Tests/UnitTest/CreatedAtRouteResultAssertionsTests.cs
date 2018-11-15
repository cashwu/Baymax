using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class CreatedAtRouteResultAssertionsTests
    {
        [Fact]
        public void WithRouteValue_Value()
        {
            new CreatedAtRoutedResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldBeCreatedAtRouteResult()
                    .WithRouteValue("id", 1)
                    .WithValue(new { data = 123 });
        }

        
        [Fact]
        public void WithRouteName_RouteValue_Value()
        {
            new CreatedAtRoutedResultController()
                    .AsTester()
                    .Action(c => c.Action2())
                    .ShouldBeCreatedAtRouteResult()
                    .WithRouteName("routeName")
                    .WithRouteValue("id", 1)
                    .WithValue(new { data = 123 });
        }
    }

    public class CreatedAtRoutedResultController : Controller
    {
        public IActionResult Action()
        {
            return new CreatedAtRouteResult(new { id = 1 }, new { data = 123 });
        }
        
        public IActionResult Action2()
        {
            return new CreatedAtRouteResult("routeName", new { id = 1 }, new { data = 123 });
        }
    }
}