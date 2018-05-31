using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class ContentResultAssertions<TController> where TController : Controller
    {
        private readonly ContentResult _contentResult;
        private readonly TController _controller;

        public ContentResultAssertions(ContentResult contentResult, TController controller)
        {
            _contentResult = contentResult;
            _controller = controller;
        }
    }
}