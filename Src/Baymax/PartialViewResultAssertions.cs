using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class PartialViewResultAssertions<TController> where TController : Controller
    {
        private readonly PartialViewResult _partialViewResult;
        private readonly Controller _controller;

        public PartialViewResultAssertions(PartialViewResult partialViewResult, Controller controller)
        {
            _partialViewResult = partialViewResult;
            _controller = controller;
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
            _partialViewResult.ViewName.Should().BeEmpty();

            return this;
        }

        public PartialViewResultAssertions<TController> WithViewBag(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_partialViewResult.ViewData[key]);

            return this;
        }

        public PartialViewResultAssertions<TController> WithNotTempData()
        {
            _partialViewResult.TempData.Count.Should().Be(0);

            return this;
        }

        public PartialViewResultAssertions<TController> WithTempData(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_partialViewResult.TempData[key]);

            return this;
        }
    }
}