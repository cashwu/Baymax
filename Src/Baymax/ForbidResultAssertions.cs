using System.Collections.Generic;
using ExpectedObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class ForbidResultAssertions<TController> where TController : Controller
    {
        private readonly ForbidResult _forbidResult;
        private readonly TController _controller;

        public ForbidResultAssertions(ForbidResult forbidResult, TController controller)
        {
            _forbidResult = forbidResult;
            _controller = controller;
        }
        
        public ForbidResultAssertions<TController> WithAuthenticationSchemes(List<string> expectedAuthenticationSchemes)
        {
            expectedAuthenticationSchemes.ToExpectedObject().ShouldEqual(_forbidResult.AuthenticationSchemes);

            return this;
        }
        
        public ForbidResultAssertions<TController> WithAuthenticationProperties(AuthenticationProperties expectedAuthenticationProperties)
        {
           expectedAuthenticationProperties.ToExpectedObject().ShouldEqual(_forbidResult.Properties);

           return this;
        }
    }
}