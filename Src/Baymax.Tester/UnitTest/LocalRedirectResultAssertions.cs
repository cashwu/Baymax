using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class LocalRedirectResultAssertions<TController> where TController : Controller
    {
        private readonly LocalRedirectResult _localRedirectResult;

        public LocalRedirectResultAssertions(LocalRedirectResult localRedirectResult)
        {
            _localRedirectResult = localRedirectResult;
        }
        
        public LocalRedirectResultAssertions<TController> WithUrl(string expectedUrl)
        {
            _localRedirectResult.Url.Should().Be(expectedUrl);

            return this;
        }
        
        public LocalRedirectResultAssertions<TController> WithPermanent(bool expectedPermanent)
        {
            _localRedirectResult.Permanent.Should().Be(expectedPermanent);

            return this;
        }
        
        public LocalRedirectResultAssertions<TController> WithPreserveMethod(bool expectedPreserveMethod)
        {
            _localRedirectResult.PreserveMethod.Should().Be(expectedPreserveMethod);

            return this;
        }
    }
}
