using System;
using System.Collections.Generic;
using System.Linq;
using Baymax.Entity;
using Baymax.Extension.Entity;
using ExpectedObjects;
using FluentAssertions;
using Xunit;

namespace Baymax.Tests.Extension
{
    public class IEnumerablePagedListExtensionTests
    {
        [Fact]
        public void ToPageList()
        {
            var pagedList = GivenData().ToPagedList(1, 2);

            new PagedList<Data>
                    {
                        PageSize = 2,
                        PageIndex = 1,
                        TotalPages = 5,
                        TotalCount = 10,
                        IndexFrom = 0,
                        Items = new List<Data>
                        {
                            new Data { Id = 2, Name = "2" },
                            new Data { Id = 3, Name = "3" }
                        }
                    }.ToExpectedObject()
                     .ShouldEqual(pagedList);
        }

        [Fact]
        public void ToPageList_ConvertToAnotherType()
        {
            Func<IEnumerable<Data>, IEnumerable<DataPage>> converter = data =>
            {
                return data.Select(a => new DataPage
                {
                    Id = a.Id,
                    Name = a.Name + "TT"
                });
            };

            var pagedList = GivenData().ToPagedList(converter, 2, 2);

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