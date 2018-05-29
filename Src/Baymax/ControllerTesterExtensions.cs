using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public static class ControllerTesterExtensions
    {
        public static ControllerTester<TController> AsTester<TController>(this TController controller)
            where TController : Controller
        {
            return new ControllerTester<TController>(controller);
        }
    }
}