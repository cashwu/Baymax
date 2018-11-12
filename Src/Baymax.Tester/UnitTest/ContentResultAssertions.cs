using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class ContentResultAssertions
    {
        private readonly ContentResult _contentResult;

        public ContentResultAssertions(ContentResult contentResult)
        {
            _contentResult = contentResult;
        }

        public ContentResultAssertions WithContent(string expectedContent)
        {
            _contentResult.Content.Should().Be(expectedContent);

            return this;
        }

        public ContentResultAssertions WithContentType(string expectedContentType)
        {
            _contentResult.ContentType.Should().Be(expectedContentType);

            return this;
        }
    }
}