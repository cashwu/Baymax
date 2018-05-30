using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class ViewResultAssertions<TController> where TController : Controller
    {
        private readonly ViewResult _viewResult;
        private readonly TController _controller;

        public ViewResultAssertions(ViewResult viewResult, TController controller)
        {
            _viewResult = viewResult;
            _controller = controller;
        }

        public ViewResultAssertions<TController> WithModel<T>(T expected)
        {
            _viewResult.Model.Should().BeAssignableTo<T>();

            expected.ToExpectedObject().ShouldEqual(_viewResult.Model);

            return this;
        }

        public ViewResultAssertions<TController> WithViewName(string viewName)
        {
            _viewResult.ViewName.Should().Be(viewName);

            return this;
        }

        public ViewResultAssertions<TController> WithViewBag(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_viewResult.ViewData[key]);

            return this;
        }

        public ViewResultAssertions<TController> WithNotTempData()
        {
            _viewResult.TempData.Count.Should().Be(0);

            return this;
        }

        public ViewResultAssertions<TController> WithTempData(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_viewResult.TempData[key]);

            return this;
        }
    }
}