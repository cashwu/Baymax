using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class RedirectToActionResultAssertions<TController> where TController : Controller
    {
        private readonly RedirectToActionResult _actionResult;
        private readonly TController _controller;

        public RedirectToActionResultAssertions(RedirectToActionResult actionResult, TController controller)
        {
            _actionResult = actionResult;
            _controller = controller;
        }

        public RedirectToActionResultAssertions<TController> WithAction(string expectedAction)
        {
            _actionResult.ActionName.Should().Be(expectedAction);

            return this;
        }

        public RedirectToActionResultAssertions<TController> WithController(string expectedController)
        {
            _actionResult.ControllerName.Should().Be(expectedController);

            return this;
        }

        public RedirectToActionResultAssertions<TController> WithRouteValue(string key, object expectedValue)
        {
            _actionResult.RouteValues[key].Should().Be(expectedValue);

            return this;
        }

        public RedirectToActionResultAssertions<TController> WithTempData(string key, object expectedValue)
        {
            _controller.TempData[key].Should().Be(expectedValue);

            return this;
        }

        public RedirectToActionResultAssertions<TController> WithNotTempData()
        {
            _controller.TempData.Count.Should().Be(0);

            return this;
        }
    }
}