using System.Collections.Generic;
using ExpectedObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class ChallengeResultAssertions  
    {
        private readonly ChallengeResult _challengeResult;

        public ChallengeResultAssertions(ChallengeResult challengeResult)
        {
            _challengeResult = challengeResult;
        }
        
        public ChallengeResultAssertions WithAuthenticationSchemes(List<string> expectedAuthenticationSchemes)
        {
            expectedAuthenticationSchemes.ToExpectedObject().ShouldEqual(_challengeResult.AuthenticationSchemes);

            return this;
        }
        
        public ChallengeResultAssertions WithAuthenticationProperties(AuthenticationProperties expectedAuthenticationProperties)
        {
           expectedAuthenticationProperties.ToExpectedObject().ShouldEqual(_challengeResult.Properties);
            
           return this;
        }
    }
}