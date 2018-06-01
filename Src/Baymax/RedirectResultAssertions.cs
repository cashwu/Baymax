using FluentAssertions;
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
        
        public RedirectResultAssertions<TController> WithUrl(string expectedUrl)
        {
            _redirectResult.Url.Should().Be(expectedUrl);

            return this;
        }
        
        public RedirectResultAssertions<TController> WithPermanent(bool expectedPermanent)
        {
            _redirectResult.Permanent.Should().Be(expectedPermanent);

            return this;
        }
    }
}