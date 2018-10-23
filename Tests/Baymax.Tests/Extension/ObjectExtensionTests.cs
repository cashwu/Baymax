using Baymax.Extension;
using FluentAssertions;
using Xunit;

namespace Baymax.Tests.Extension
{
    public class ObjectExtensionTests
    {
        [Fact]
        public void ToNotNullString()
        {
            string str = null;
            
            str.ToNotNullString().Should().BeEmpty();
            
            str.ToNotNullString("empty").Should().Be("empty");

            1.ToNotNullString().Should().Be("1");
        }
    }
}