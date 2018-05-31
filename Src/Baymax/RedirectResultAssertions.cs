using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class RedirectResultAssertions<TController> where TController : Controller
    {
        private readonly RedirectResult _redirectResult;
        private readonly TController _controller;

        public RedirectResultAssertions(RedirectResult redirectResult, TController controller)
        {
            _redirectResult = redirectResult;
            _controller = controller;
        }
    }
}