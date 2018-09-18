using System.Collections.Generic;
using ExpectedObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class ForbidResultAssertions
    {
        private readonly ForbidResult _forbidResult;

        public ForbidResultAssertions(ForbidResult forbidResult)
        {
            _forbidResult = forbidResult;
        }
        
        public ForbidResultAssertions WithAuthenticationSchemes(List<string> expectedAuthenticationSchemes)
        {
            expectedAuthenticationSchemes.ToExpectedObject().ShouldEqual(_forbidResult.AuthenticationSchemes);

            return this;
        }
        
        public ForbidResultAssertions WithAuthenticationProperties(AuthenticationProperties expectedAuthenticationProperties)
        {
           expectedAuthenticationProperties.ToExpectedObject().ShouldEqual(_forbidResult.Properties);

           return this;
        }
    }
}