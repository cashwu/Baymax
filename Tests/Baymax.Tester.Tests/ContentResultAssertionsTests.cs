using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Sdk;

namespace Baymax.Tester.Tests
{
    public class ContentResultAssertionsTests
    {
        [Fact]
        public void Result_with_content_assertWithContent_should_not_throw_exception()
        {
            var contentResult = new ContentResult
            {
                Content = "Test"
            };

            var assertions = GivenContentResultAssertions(contentResult);

            assertions = assertions.WithContent("Test");

            assertions.Should().NotBeNull();
        }
        
        
        [Fact]
        public void Result_not_content_assertWithContent_should_throw_exception()
        {
            var contentResult = new ContentResult();

            var assertions = GivenContentResultAssertions(contentResult);

            Assert.Throws<XunitException>(() => assertions.WithContent("Test"));
        }

        private ContentResultAssertions GivenContentResultAssertions(ContentResult contentResult)
        {
            return new ContentResultAssertions(contentResult);
        }
    }
}