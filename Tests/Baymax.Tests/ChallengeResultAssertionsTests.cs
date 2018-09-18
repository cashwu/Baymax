using System.Collections.Generic;
using Baymax.Tester.UnitTest;
using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests
{
    public class ChallengeResultAssertionsTests
    {
        [Fact]
        public void Result_with_authentication_schemes_assertWithAuthenticationSchemes_should_not_throw_exception()
        {
            var challengeResult = new ChallengeResult
            {
                AuthenticationSchemes = new List<string> {"Auth1", "Auth2"}
            };

            var assertions = GivenChallengeResultAssertions(challengeResult);

            assertions = assertions.WithAuthenticationSchemes(new List<string> {"Auth1", "Auth2"});

            assertions.Should().NotBeNull();
        }

        [Fact]
        public void Result_not_authentication_schemes_assertWithAuthenticationSchemes_should_throw_exception()
        {
            var challengeResult = new ChallengeResult();

            var assertions = GivenChallengeResultAssertions(challengeResult);

            Assert.Throws<ComparisonException>(() =>
                assertions.WithAuthenticationSchemes(new List<string> {"Auth1", "Auth2"}));
        }

        [Fact]
        public void
            Result_with_authentication_properties_assertWithAuthenticationProperties_should_not_throw_exception()
        {
            var challengeResult = new ChallengeResult
            {
                Properties = new AuthenticationProperties
                {
                    RedirectUri = "http://test.com",
                    Items = {{"key", "value"}}
                }
            };

            var assertions = GivenChallengeResultAssertions(challengeResult);

            assertions = assertions.WithAuthenticationProperties(new AuthenticationProperties
            {
                RedirectUri = "http://test.com",
                Items = {{"key", "value"}}
            });

            assertions.Should().NotBeNull();
        }

        [Fact]
        public void Result_not_authentication_properties_assertWithAuthenticationProperties_should_throw_exception()
        {
            var challengeResult = new ChallengeResult();

            var assertions = GivenChallengeResultAssertions(challengeResult);

            Assert.Throws<ComparisonException>(() =>
                assertions.WithAuthenticationProperties(new AuthenticationProperties
                {
                    RedirectUri = "http://test.com",
                    Items = {{"key", "value"}}
                }));
        }


        private ChallengeResultAssertions GivenChallengeResultAssertions(ChallengeResult challengeResult)
        {
            return new ChallengeResultAssertions(challengeResult);
        }
    }
}