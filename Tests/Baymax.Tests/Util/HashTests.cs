using System;
using Baymax.Util;
using FluentAssertions;
using Xunit;

namespace Baymax.Tests.Util
{
    public class HashTests
    {
        [Fact]
        public void Create()
        {
            var hash = Hash.Create("cash", "salt");

            hash.Should().NotBeEmpty();

            var hash2 = Hash.Create("cash", "salt");

            hash.Should().BeEquivalentTo(hash2);
        }

        [Fact]
        public void Validate()
        {
            var hash = Hash.Create("cash", "salt");

            Hash.Validate("cash", "salt", hash).Should().BeTrue();

            Hash.Validate("cash1", "salt", hash).Should().BeFalse();
        }

        [Fact]
        public void ArgumentNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
                  {
                      Hash.Create("", "salt");
                  })
                  .Message
                  .Should()
                  .Contain("value");
        }
        
        [Fact]
        public void ArgumentNullSalt()
        {
            Assert.Throws<ArgumentNullException>(() =>
                  {
                      Hash.Create("cash", "");
                  })
                  .Message
                  .Should()
                  .Contain("salt");
        }
    }
}