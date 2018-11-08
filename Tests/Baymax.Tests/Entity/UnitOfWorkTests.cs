using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Baymax.Entity.Interface;
using ExpectedObjects;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Baymax.Tests.Entity
{
    public class UnitOfWorkTests
    {
        private readonly ITestUnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            _unitOfWork = new ServiceCollection()
                          .AddDbContext<TestDbContext>(options =>
                          {
                              options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                          })
                          .AddScoped<ITestUnitOfWork, TestUnitOfWork>()
                          .BuildServiceProvider()
                          .GetRequiredService<ITestUnitOfWork>();
        }

        [Fact]
        public void Init()
        {
            _unitOfWork.DbContext.Should().NotBeNull();
            var repo = _unitOfWork.GetRepository<Person>();
            repo.Should().NotBeNull();

            var repo2 = _unitOfWork.GetRepository<Person>();
            repo.Should().BeSameAs(repo2);

            var repoView = _unitOfWork.GetViewRepository<PersonView>();
            repoView.Should().NotBeNull();

            var repoView2 = _unitOfWork.GetViewRepository<PersonView>();
            repoView.Should().BeSameAs(repoView2);
        }

        [Fact]
        public void Repository_Insert()
        {
            var repo = _unitOfWork.GetRepository<Person>();

            repo.Insert(new Person { Id = 1, Name = "a" });

            repo.Insert(new Person { Id = 2, Name = "b" }, new Person { Id = 3, Name = "c" });

            repo.Insert(new List<Person>
            {
                new Person { Id = 4, Name = "d" },
                new Person { Id = 5, Name = "e" }
            });

            repo.InsertAsync(new Person { Id = 6, Name = "f" });

            repo.InsertAsync(new Person { Id = 7, Name = "g" }, new Person { Id = 8, Name = "h" });

            repo.InsertAsync(new List<Person>
            {
                new Person { Id = 9, Name = "i" },
                new Person { Id = 10, Name = "j" }
            });

            _unitOfWork.Commit();

            var persons = repo.GetAll().ToList();

            persons.Count.Should().Be(10);
        }

        [Fact]
        public async Task Repository_FirstOrDefault()
        {
            GivenPersonData();

            var person = _unitOfWork.GetRepository<Person>().GetFirstOrDefault();
            person.Id.Should().Be(1);

            var person2 = await _unitOfWork.GetRepository<Person>().GetFirstOrDefaultAsync();
            person2.Id.Should().Be(1);

            var name = _unitOfWork.GetRepository<Person>()
                                  .GetFirstOrDefault(predicate: a => a.Id == 2,
                                                     selector: a => a.Name,
                                                     disableTracking: true);

            name.Should().Be("b");

            var name2 = await _unitOfWork.GetRepository<Person>()
                                         .GetFirstOrDefaultAsync(predicate: a => a.Id == 2,
                                                                 selector: a => a.Name,
                                                                 disableTracking: true);

            name2.Should().Be("b");

            var d = _unitOfWork.GetRepository<Person>()
                               .GetFirstOrDefault(include: a => a.Include(b => b.Phones),
                                                  selector: a => new
                                                  {
                                                      a.Id,
                                                      Phone = a.Phones.First().Number
                                                  },
                                                  orderBy: a => a.OrderBy(b => b.Id),
                                                  disableTracking: true);

            d.Id.Should().Be(1);
            d.Phone.Should().Be("123");

            var d2 = await _unitOfWork.GetRepository<Person>()
                                      .GetFirstOrDefaultAsync(include: a => a.Include(b => b.Phones),
                                                              selector: a => new
                                                              {
                                                                  a.Id,
                                                                  Phone = a.Phones.First().Number
                                                              },
                                                              orderBy: a => a.OrderBy(b => b.Id),
                                                              disableTracking: true);

            d2.Id.Should().Be(1);
            d2.Phone.Should().Be("123");
        }

        [Fact]
        public async Task Repository_GetAll()
        {
            GivenPersonData();

            var names = _unitOfWork.GetRepository<Person>()
                                   .GetAll(selector: a => a.Name,
                                           predicate: a => a.Id > 1,
                                           orderBy: a => a.OrderByDescending(b => b.Id),
                                           include: a => a.Include(b => b.Phones),
                                           disableTracking: true)
                                   .ToList();

            new List<string> { "b", "ab" }.ToExpectedObject().ShouldEqual(names);

            var persons = _unitOfWork.GetRepository<Person>()
                                     .GetAll(predicate: a => a.Id > 1,
                                             orderBy: a => a.OrderBy(b => b.Id),
                                             include: a => a.Include(b => b.Phones),
                                             disableTracking: true)
                                     .ToList();

            new List<Person>
                    {
                        new Person
                        {
                            Id = 2, Name = "b", Phones = new HashSet<Phone>
                            {
                                new Phone { Id = 3, Number = "321" },
                                new Phone { Id = 4, Number = "654" }
                            }
                        },
                        new Person
                        {
                            Id = 3, Name = "ab", Phones = new HashSet<Phone>()
                        }
                    }.ToExpectedObject()
                     .ShouldEqual(persons);
        }

        [Fact]
        public async Task Repository_GetPageList()
        {
            GivenPersonData();

            new PagedList<string>
                    {
                        PageSize = 2,
                        PageIndex = 0,
                        TotalPages = 2,
                        TotalCount = 3,
                        IndexFrom = 0,
                        Items = new List<string> { "ab", "b" }
                    }.ToExpectedObject()
                     .ShouldEqual(PagedList(0, 2));

            new PagedList<string>
                    {
                        PageSize = 2,
                        PageIndex = 1,
                        TotalPages = 2,
                        TotalCount = 3,
                        IndexFrom = 0,
                        Items = new List<string> { "a" }
                    }.ToExpectedObject()
                     .ShouldEqual(PagedList(1, 2));

            new PagedList<string>
                    {
                        PageSize = 2,
                        PageIndex = 0,
                        TotalPages = 2,
                        TotalCount = 3,
                        IndexFrom = 0,
                        Items = new List<string> { "ab", "b" }
                    }.ToExpectedObject()
                     .ShouldEqual(await PagedListAsync(0, 2));

            new PagedList<string>
                    {
                        PageSize = 2,
                        PageIndex = 1,
                        TotalPages = 2,
                        TotalCount = 3,
                        IndexFrom = 0,
                        Items = new List<string> { "a" }
                    }.ToExpectedObject()
                     .ShouldEqual(await PagedListAsync(1, 2));

            var data2 = _unitOfWork.GetRepository<Person>()
                                   .GetPagedList(predicate: a => a.Id > 0,
                                                 orderBy: a => a.OrderByDescending(b => b.Id),
                                                 include: a => a.Include(b => b.Phones),
                                                 pageIndex: 0,
                                                 pageSize: 2,
                                                 disableTracking: true);

            new PagedList<Person>
                    {
                        PageSize = 2,
                        PageIndex = 0,
                        TotalPages = 2,
                        TotalCount = 3,
                        IndexFrom = 0,
                        Items = Persons().OrderByDescending(a => a.Id).Take(2).ToList()
                    }.ToExpectedObject()
                     .ShouldEqual(data2);

            data2 = await _unitOfWork.GetRepository<Person>()
                                     .GetPagedListAsync(predicate: a => a.Id > 0,
                                                        orderBy: a => a.OrderByDescending(b => b.Id),
                                                        include: a => a.Include(b => b.Phones),
                                                        pageIndex: 0,
                                                        pageSize: 2,
                                                        disableTracking: true);

            new PagedList<Person>
                    {
                        PageSize = 2,
                        PageIndex = 0,
                        TotalPages = 2,
                        TotalCount = 3,
                        IndexFrom = 0,
                        Items = Persons().OrderByDescending(a => a.Id).Take(2).ToList()
                    }.ToExpectedObject()
                     .ShouldEqual(data2);

            IPagedList<string> PagedList(int pageIndex, int pageSize)
            {
                return _unitOfWork.GetRepository<Person>()
                                  .GetPagedList(selector: a => a.Name,
                                                predicate: a => a.Id > 0,
                                                orderBy: a => a.OrderByDescending(b => b.Id),
                                                pageIndex: pageIndex,
                                                pageSize: pageSize,
                                                disableTracking: true);
            }

            async Task<IPagedList<string>> PagedListAsync(int pageIndex, int pageSize)
            {
                return await _unitOfWork.GetRepository<Person>()
                                        .GetPagedListAsync(selector: a => a.Name,
                                                           predicate: a => a.Id > 0,
                                                           orderBy: a => a.OrderByDescending(b => b.Id),
                                                           pageIndex: pageIndex,
                                                           pageSize: pageSize,
                                                           disableTracking: true);
            }
        }

        [Fact]
        public async Task Repository_Find()
        {
            GivenPersonData();

            var person = _unitOfWork.GetRepository<Person>()
                                    .Find(1);

            Persons().First().ToExpectedObject().ShouldEqual(person);

            person = await _unitOfWork.GetRepository<Person>()
                                      .FindAsync(1);

            Persons().First().ToExpectedObject().ShouldEqual(person);

            person = await _unitOfWork.GetRepository<Person>()
                                      .FindAsync(new object[] { 1 }, CancellationToken.None);

            Persons().First().ToExpectedObject().ShouldEqual(person);
        }

        private void GivenPersonData()
        {
            _unitOfWork.GetRepository<Person>().Insert(Persons());
            _unitOfWork.Commit();
        }

        private List<Person> Persons()
        {
            return new List<Person>
            {
                new Person
                {
                    Id = 1, Name = "a", Phones = new HashSet<Phone>
                    {
                        new Phone { Id = 1, Number = "123" },
                        new Phone { Id = 2, Number = "456" }
                    }
                },
                new Person
                {
                    Id = 2, Name = "b", Phones = new HashSet<Phone>
                    {
                        new Phone { Id = 3, Number = "321" },
                        new Phone { Id = 4, Number = "654" }
                    }
                },
                new Person
                {
                    Id = 3, Name = "ab",
                    Phones = new HashSet<Phone>()
                }
            };
        }
    }
}