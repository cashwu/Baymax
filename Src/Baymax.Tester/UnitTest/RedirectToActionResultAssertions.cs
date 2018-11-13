using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
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

        public RedirectToActionResultAssertions<TController> WithFragment(string expectedFragment)
        {
            _actionResult.Fragment.Should().Be(expectedFragment);

            return this;
        }
        
        public RedirectToActionResultAssertions<TController> WithTempData(string key, object expectedValue)
        {
            _controller.TempData[key].Should().Be(expectedValue);

            return this;
        }

        public RedirectToActionResultAssertions<TController> WithNotTempData()
        {
            _controller.TempData.Should().BeNull();

            return this;
        }

        public RedirectToActionResultAssertions<TController> WithPermanent(bool expectedPermanent)
        {
            _actionResult.Permanent.Should().Be(expectedPermanent);
            
            return this;
        }

        public RedirectToActionResultAssertions<TController> WithPreserveMethod(bool expectedPreserveMethod)
        {
            _actionResult.PreserveMethod.Should().Be(expectedPreserveMethod);
            
            return this;
        }
    }
}