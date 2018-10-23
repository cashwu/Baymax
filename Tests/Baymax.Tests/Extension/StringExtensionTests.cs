using System.Collections.Generic;
using Baymax.Extension;
using FluentAssertions;
using Xunit;

namespace Baymax.Tests.Extension
{
    public class StringExtensionTests
    {
        [Fact]
        public void SplitToList()
        {
            var expectations = new List<string> { "1", "2", "3" };
            
            "1,2,3".SplitToList().Should().BeEquivalentTo(expectations);
            
            "1,2,3,".SplitToList().Should().BeEquivalentTo(expectations);
            
            "1;2;3".SplitToList(';').Should().BeEquivalentTo(expectations);

            "".SplitToList().Should().BeEquivalentTo(new List<string>());
        }

        [Fact]
        public void EqualIgnoreCase()
        {
            "a".EqualsIgnoreCase("a").Should().BeTrue();
            "A".EqualsIgnoreCase("a").Should().BeTrue();

            "a".EqualsIgnoreCase("b").Should().BeFalse();
            "A".EqualsIgnoreCase("b").Should().BeFalse();
        }
    }
}