using System.Collections.Generic;
using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Baymax.Tester.Tests
{
    public class ForbidResultAssertionsTests
    {
        [Fact]
        public void Result_with_authentication_schemes_assertWithAuthenticationSchemes_should_not_throw_exception()
        {
            var forbidesult = new ForbidResult
            {
                AuthenticationSchemes = new List<string> {"Auth1", "Auth2"}
            };

            var assertions = GivenForbidResultAssertions(forbidesult);

            assertions = assertions.WithAuthenticationSchemes(new List<string> {"Auth1", "Auth2"});

            assertions.Should().NotBeNull();
        }

        [Fact]
        public void Result_not_authentication_schemes_assertWithAuthenticationSchemes_should_throw_exception()
        {
            var forbidResult = new ForbidResult();

            var assertions = GivenForbidResultAssertions(forbidResult);

            Assert.Throws<ComparisonException>(() =>
                assertions.WithAuthenticationSchemes(new List<string> {"Auth1", "Auth2"}));
        }
        
        [Fact]
        public void Result_with_authentication_properties_assertWithAuthenticationProperties_should_not_throw_exception()
        {
            var forbidesult = new ForbidResult
            {
                Properties = new AuthenticationProperties
                {
                    RedirectUri = "http://test.com",
                    Items = {{"key", "value"}}
                }
            };

            var assertions = GivenForbidResultAssertions(forbidesult);

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
            var forbidesult = new ForbidResult();

            var assertions = GivenForbidResultAssertions(forbidesult);

            Assert.Throws<ComparisonException>(() =>
                assertions.WithAuthenticationProperties(new AuthenticationProperties
                {
                    RedirectUri = "http://test.com",
                    Items = {{"key", "value"}}
                }));
        }
        
        private ForbidResultAssertions GivenForbidResultAssertions(ForbidResult forbidesult)
        {
            return new ForbidResultAssertions(forbidesult);
        }
    }
}