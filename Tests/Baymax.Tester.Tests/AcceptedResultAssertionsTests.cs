using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Sdk;

namespace Baymax.Tester.Tests
{
    public class AcceptedResultAssertionsTests : ResultAssertionTestBase
    {
        [Fact]
        public void Result_with_location_assertWithLocation_should_not_throw_exception()
        {
            var acceptedResult = new AcceptedResult
            {
               Location = "http://test.com"
            }; 
            
            var assertions = GivenAcceptedResultAssertions(acceptedResult);

            assertions = assertions.WithLocation("http://test.com");

            assertions.Should().NotBeNull();
        }

        [Fact]
        public void Result_not_location_assertWithLocation_should_throw_exception()
        {
            var acceptedResult = new AcceptedResult();

            var assertions = GivenAcceptedResultAssertions(acceptedResult);

            Assert.Throws<XunitException>(() => assertions.WithLocation("http://test.com"));
        }
        
        [Fact]
        public void Result_with_value_assertWithValue_should_not_throw_exception()
        {
            var acceptedResult = new AcceptedResult
            {
               Value = new TestModel
               {
                   Id = 1,
                   Name = "Test"
               }
            };

            var assertions = GivenAcceptedResultAssertions(acceptedResult);

            assertions = assertions.WithValue(new TestModel
            {
                Id = 1,
                Name = "Test"
            });

            assertions.Should().NotBeNull();
        }
        
        [Fact]
        public void Result_not_value_assertWithValue_should_throw_exception()
        {
            var acceptedResult = new AcceptedResult();

            var assertions = GivenAcceptedResultAssertions(acceptedResult);

            Assert.Throws<ComparisonException>(() => assertions.WithValue(new TestModel
            {
                Id = 1,
                Name = "Test"
            }));
            
        }
        
        private AcceptedResultAssertions<Controller> GivenAcceptedResultAssertions(AcceptedResult acceptedResult)
        {
            return new AcceptedResultAssertions<Controller>(acceptedResult);
        }
    }
}