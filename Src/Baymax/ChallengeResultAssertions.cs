using System.Collections.Generic;
using ExpectedObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class ChallengeResultAssertions<TController> where TController : Controller
    {
        private readonly ChallengeResult _challengeResult;
        private readonly TController _controller;

        public ChallengeResultAssertions(ChallengeResult challengeResult, TController controller)
        {
            _challengeResult = challengeResult;
            _controller = controller;
        }
        
        public ChallengeResultAssertions<TController> WithAuthenticationSchemes(List<string> expectedAuthenticationSchemes)
        {
            expectedAuthenticationSchemes.ToExpectedObject().ShouldEqual(_challengeResult.AuthenticationSchemes);

            return this;
        }
        
        public ChallengeResultAssertions<TController> WithAuthenticationProperties(AuthenticationProperties expectedAuthenticationProperties)
        {
           expectedAuthenticationProperties.ToExpectedObject().ShouldEqual(_challengeResult.Properties);
            
           return this;
        }
    }
}