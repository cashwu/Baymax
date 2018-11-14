using System;
using Baymax.Tester.UnitTest;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests.UnitTest
{
    public class AcceptedResultAssertionsTests
    {
        [Fact]
        public void WithLocation_Value()
        {
            new AcceptedResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldAcceptedResult()
                    .WithLocation("location")
                    .WithValue(new { Id = 1 });
        }
        
        [Fact]
        public void WithLocation_Value_2()
        {
            new AcceptedResultController()
                    .AsTester()
                    .Action(c => c.Action())
                    .ShouldAcceptedResult()
                    .WithLocation("location")
                    .WithValue(new { Id = 1 });
        }
    }

    public class AcceptedResultController : Controller
    {
        public IActionResult Action()
        {
            return new AcceptedResult("location", new { Id = 1 });
        }
        
        public IActionResult Action1()
        {
            return new AcceptedResult(new Uri("location"), new { Id = 1 });
        }
    }
}