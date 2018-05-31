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
    }
}