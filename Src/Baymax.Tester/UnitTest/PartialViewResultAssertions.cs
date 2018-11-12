using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class PartialViewResultAssertions<TController> where TController : Controller
    {
        private readonly PartialViewResult _partialViewResult;

        public PartialViewResultAssertions(PartialViewResult partialViewResult)
        {
            _partialViewResult = partialViewResult;
        }
        
        public PartialViewResultAssertions<TController> WithModel<T>(T expected)
        {
            _partialViewResult.Model.Should().BeAssignableTo<T>();

            expected.ToExpectedObject().ShouldEqual(_partialViewResult.Model);

            return this;
        }

        public PartialViewResultAssertions<TController> WithViewName(string viewName)
        {
            _partialViewResult.ViewName.Should().Be(viewName);

            return this;
        }
        
        public PartialViewResultAssertions<TController> WithDefaultViewName()
        {
            _partialViewResult.ViewName.Should().BeNull();

            return this;
        }

        public PartialViewResultAssertions<TController> WithViewBagOrViewData(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_partialViewResult.ViewData[key]);

            return this;
        }

        public PartialViewResultAssertions<TController> WithNotTempData()
        {
            _partialViewResult.TempData.Should().BeNull();

            return this;
        }

        public PartialViewResultAssertions<TController> WithTempData(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_partialViewResult.TempData[key]);

            return this;
        }
    }
}
