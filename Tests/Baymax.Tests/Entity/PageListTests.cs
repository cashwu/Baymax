using System;
using System.Collections.Generic;
using System.Linq;
using Baymax.Entity;
using Baymax.Extension.Entity;
using ExpectedObjects;
using FluentAssertions;
using Xunit;

namespace Baymax.Tests.Entity
{
    public class PageListTests
    {
        [Fact]
        public void ToEmptyPageList()
        {
            var pagedList = PagedList.Empty<Data>();

            pagedList.PageSize.Should().Be(0);
            pagedList.PageIndex.Should().Be(0);
            pagedList.TotalPages.Should().Be(0);
            pagedList.TotalCount.Should().Be(0);
            pagedList.IndexFrom.Should().Be(0);
            pagedList.HasNextPage.Should().BeFalse();
            pagedList.HasPreviousPage.Should().BeFalse();
            pagedList.Items.Should().BeEmpty();
        }
        
        [Fact]
        public void ToAnotherPageList()
        {
            var originPagedList = GivenData().ToPagedList(2, 2);
            
            Func<IEnumerable<Data>, IEnumerable<DataPage>> converter = data =>
            {
                return data.Select(a => new DataPage
                {
                    Id = a.Id,
                    Name = a.Name + "TT"
                });
            };

            var pagedList = PagedList.From(originPagedList, converter);
            
            pagedList.PageSize.Should().Be(2);
            pagedList.PageIndex.Should().Be(2);
            pagedList.TotalPages.Should().Be(5);
            pagedList.TotalCount.Should().Be(10);
            pagedList.IndexFrom.Should().Be(0);
            pagedList.HasNextPage.Should().BeTrue();
            pagedList.HasPreviousPage.Should().BeTrue();

            new List<DataPage>
                    {
                        new DataPage { Id = 4, Name = "4TT" },
                        new DataPage { Id = 5, Name = "5TT" }
                    }.ToExpectedObject()
                     .ShouldEqual(pagedList.Items);
        }

        private static IEnumerable<Data> GivenData()
        {
            for (var i = 0; i < 10; i++)
            {
                yield return new Data { Id = i, Name = i.ToString() };
            }
        }
    }
}