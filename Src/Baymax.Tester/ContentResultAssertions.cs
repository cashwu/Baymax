using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester
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
    }
}