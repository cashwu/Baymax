using System.Collections.Generic;
using System.Linq;
using Baymax.Util;
using ExpectedObjects;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Baymax.Tests.Util
{
    public class EnumerationTests
    {
        [Fact]
        public void Value_DisplayName()
        {
            var e = EnumTest.A;
            e.Value.Should().Be(1);
            e.DisplayName.Should().Be("A");
        }

        [Fact]
        public void GetAll()
        {
            var list = EnumTest.GetAll().ToList();

            var expect = new List<EnumTest>
            {
                EnumTest.A,
                EnumTest.B
            };

            expect.ToExpectedObject().ShouldEqual(list);
        }

        [Fact]
        public void FromValue()
        {
            var e = EnumTest.FromValue(1);
            EnumTest.A.ToExpectedObject().ShouldEqual(e);
        }

        [Fact]
        public void FromValue_Error()
        {
            var e = EnumTest.FromValue(3);
            e.Should().BeNull();
        }

        [Fact]
        public void FromDisplayName()
        {
            var e = EnumTest.FromDisplayName("A");
            EnumTest.A.ToExpectedObject().ShouldEqual(e);
        }

        [Fact]
        public void FromDisplayName_not_case()
        {
            var e = EnumTest.FromDisplayName("a");
            EnumTest.A.ToExpectedObject().ShouldEqual(e);
        }

        [Fact]
        public void FromDisplayName_Error()
        {
            var e = EnumTest.FromDisplayName("C");
            e.Should().BeNull();
        }

        [Fact]
        public void Equal_false()
        {
            EnumTest.A.Equals(EnumTest.B).Should().BeFalse();
        }
        
        [Fact]
        public void Equal_true()
        {
            EnumTest.A.Equals(EnumTest.A).Should().BeTrue();
        }

        [Fact]
        public void _ToString()
        {
            EnumTest.A.ToString().Should().Be("A");
        }

        [Fact]
        public void CompareTo()
        {
            EnumTest.A.CompareTo(EnumTest.A).Should().Be(0);
        }

        [Fact]
        public void JsonConvert_SerializeObject()
        {
            var t = new Test
            {
                Id = 1,
                EnumTest = EnumTest.A
            };

            var str = JsonConvert.SerializeObject(t, new EnumerationJsonCovert(typeof(EnumTest)));
            str.Should().Be("{\"Id\":1,\"EnumTest\":{\"Value\":1,\"DisplayName\":\"A\"}}");
        }

        [Fact]
        public void JsonConvert_DeserializeObject()
        {
            var str = "{\"Id\":1,\"EnumTest\":{\"Value\":1,\"DisplayName\":\"A\"}}";

            var t = JsonConvert.DeserializeObject<Test>(str, new EnumerationJsonCovert(typeof(EnumTest)));

            var expect = new Test
            {
                Id = 1,
                EnumTest = EnumTest.A
            };

            expect.ToExpectedObject().ShouldEqual(t);
        }
    }

    public class Test
    {
        public int Id { get; set; }

        [JsonConverter(typeof(EnumerationJsonCovert))]
        public EnumTest EnumTest { get; set; }
    }

    public class EnumTest : Enumeration<EnumTest>
    {
        public static readonly EnumTest A = new EnumTest(1, "A");
        public static readonly EnumTest B = new EnumTest(2, "B");

        private EnumTest(int val, string name)
                : base(val, name)
        {
        }
    }
}