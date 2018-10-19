using System.Collections.Generic;
using Baymax.Extension;
using FluentAssertions;
using Xunit;

namespace Baymax.Tests.Extension
{
    public class IEnumableExtensionTests
    {
        [Fact]
        public void Array_ToJoinString()
        {
            var array = new[] { 1, 2, 3, 4 };
            array.ToJoinString(",").Should().Be("1,2,3,4");
        }

        [Fact]
        public void List_ToJoinString()
        {
            var array = new List<int> { 1, 2, 3, 4 };
            array.ToJoinString(",").Should().Be("1,2,3,4");
        }

        [Fact]
        public void CountOrDefault_Null()
        {
            List<string> list = null;
            list.CountOrDefault().Should().Be(0);
        }

        [Fact]
        public void CountOrDefault_Predicate_Null()
        {
            List<string> list = null;
            list.CountOrDefault(a => a == "A").Should().Be(0);
        }

        [Fact]
        public void AnyItem_Null()
        {
            List<string> list = null;
            list.AnyItem().Should().BeFalse();
        }

        [Fact]
        public void AnyItem_Predicate_Null()
        {
            List<string> list = null;
            list.AnyItem(a => a == "A").Should().BeFalse();
        }
    }
}