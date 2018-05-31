using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class EmptyResultAssertions<TController> where TController : Controller
    {
        private readonly EmptyResult _emptyResult;
        private readonly TController _controller;

        public EmptyResultAssertions(EmptyResult emptyResult, TController controller)
        {
            _emptyResult = emptyResult;
            _controller = controller;
        }
    }
}