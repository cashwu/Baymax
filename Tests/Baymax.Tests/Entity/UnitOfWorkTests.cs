using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Baymax.Entity.Interface;
using ExpectedObjects;
using FluentAssertions;
using Microsoft.Data.Sqlite;
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
                              options.UseSqlite("DataSource=:memory:", x =>
                              {
                              });
                          })
                          .AddScoped<ITestUnitOfWork, TestUnitOfWork>()
                          .BuildServiceProvider()
                          .GetRequiredService<ITestUnitOfWork>();

            _unitOfWork.DbContext.Database.OpenConnection();
            _unitOfWork.DbContext.Database.EnsureCreated();
        }

        [Fact]
        public void UnitOfWork_Init()
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
        public async Task UnitOfWork_Commit()
        {
            var repo = _unitOfWork.GetRepository<Person>();

            repo.Insert(new Person { Id = 1, Name = "a" });

            _unitOfWork.Commit().Should().Be(1);

            repo.Insert(new Person { Id = 2, Name = "a" });

            await _unitOfWork.CommitAsync();
            
            repo.Count().Should().Be(2);
        }
        
        [Fact]
        public void UnitOfWork_FromSql()
        {
            GivenPersonData();
            
            var command = _unitOfWork.ExecuteSqlCommand($"delete from Phone where id = {1}");

            command.Should().Be(1);
            
            var command2 = _unitOfWork.ExecuteSqlCommand("delete from Phone where id = @id", new SqliteParameter("id", 2));

            command2.Should().Be(1);
        }

        [Fact]
        public async Task Repository_Insert()
        {
            var repo = _unitOfWork.GetRepository<Person>();

            repo.Insert(new Person { Id = 1, Name = "a" });

            repo.Insert(new Person { Id = 2, Name = "b" }, new Person { Id = 3, Name = "c" });

            repo.Insert(new List<Person>
            {
                new Person { Id = 4, Name = "d" },
                new Person { Id = 5, Name = "e" }
            });

            await repo.InsertAsync(new Person { Id = 6, Name = "f" });

            await repo.InsertAsync(new Person { Id = 7, Name = "g" }, new Person { Id = 8, Name = "h" });

            await repo.InsertAsync(new List<Person>
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

            Persons().Where(a => a.Id > 1).ToList().ToExpectedObject().ShouldEqual(persons);
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

        [Fact]
        public void Repository_Count()
        {
            GivenPersonData();

            var count = _unitOfWork.GetRepository<Person>().Count();
            count.Should().Be(3);

            count = _unitOfWork.GetRepository<Person>().Count(a => a.Id > 1);
            count.Should().Be(2);
        }

        [Fact]
        public void Repository_Any()
        {
            GivenPersonData();

            var count = _unitOfWork.GetRepository<Person>().Any();
            count.Should().BeTrue();

            count = _unitOfWork.GetRepository<Person>().Any(a => a.Id > 10);
            count.Should().BeFalse();
        }

        [Fact]
        public void Repository_FromSql()
        {
            GivenPersonData();

            var person = _unitOfWork.GetRepository<Person>()
                                    .FromSql($"select * from Person where id = {1}")
                                    .FirstOrDefault();

            person.Id.Should().Be(1);

            person = _unitOfWork.GetRepository<Person>()
                                .FromSql("select * from Person where id = @id", new SqliteParameter("id", 1))
                                .FirstOrDefault();

            person.Id.Should().Be(1);
        }

        [Fact]
        public void Repository_Update()
        {
            GivenPersonData();

            var repo = _unitOfWork.GetRepository<Person>();
            var person = repo.GetFirstOrDefault(predicate: a => a.Id == 1, disableTracking: false);

            person.Name = "123";
            repo.Update(person);
            _unitOfWork.Commit();

            var p = repo.GetFirstOrDefault(predicate: a => a.Id == 1);

            new Person { Id = 1, Name = "123" }.ToExpectedObject().ShouldEqual(p);

            var persons = repo.GetAll(predicate: a => a.Id > 1, disableTracking: false).ToList();

            persons[0].Name = "222";
            persons[1].Name = "333";

            repo.Update(persons);
            _unitOfWork.Commit();

            var ps = repo.GetAll(predicate: a => a.Id > 1).ToList();

            new List<Person>
                    {
                        new Person { Id = 2, Name = "222" },
                        new Person { Id = 3, Name = "333" }
                    }.ToExpectedObject()
                     .ShouldEqual(ps);

            var persons2 = repo.GetAll(predicate: a => a.Id > 1, disableTracking: false).ToList();

            persons2[0].Name = "999";
            persons2[1].Name = "888";

            repo.Update(persons2[0], persons2[1]);
            _unitOfWork.Commit();

            var ps2 = repo.GetAll(predicate: a => a.Id > 1).ToList();

            new List<Person>
                    {
                        new Person { Id = 2, Name = "999" },
                        new Person { Id = 3, Name = "888" }
                    }.ToExpectedObject()
                     .ShouldEqual(ps2);
        }

        [Fact]
        public void Repository_Delete()
        {
            GivenPersonData();

            var repo = _unitOfWork.GetRepository<Phone>();

            repo.Delete(1);
            _unitOfWork.Commit();

            repo.Any(a => a.Id == 1).Should().BeFalse();

            var phone = repo.GetFirstOrDefault(predicate: a => a.Id == 2, disableTracking: false);

            repo.Delete(phone);
            _unitOfWork.Commit();

            repo.Any(a => a.Id == 2).Should().BeFalse();

            var phones = repo.GetAll(predicate: a => a.Id == 3 || a.Id == 4, disableTracking: false).ToList();

            repo.Delete(phones[0], phones[1]);
            _unitOfWork.Commit();

            repo.Any(a => a.Id == 3 || a.Id == 4).Should().BeFalse();
            
            var phones2 = repo.GetAll(predicate: a => a.Id == 5 || a.Id == 6, disableTracking: false);

            repo.Delete(phones2);
            _unitOfWork.Commit();

            repo.Any(a => a.Id == 5 || a.Id == 6).Should().BeFalse();
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
                    Id = 1, Name = "a",
                    Phones = new HashSet<Phone>
                    {
                        new Phone { Id = 1, Number = "123" },
                        new Phone { Id = 2, Number = "456" }
                    }
                },
                new Person
                {
                    Id = 2, Name = "b",
                    Phones = new HashSet<Phone>
                    {
                        new Phone { Id = 3, Number = "321" },
                        new Phone { Id = 4, Number = "654" }
                    }
                },
                new Person
                {
                    Id = 3, Name = "ab",
                    Phones = new HashSet<Phone>
                    {
                        new Phone { Id = 5, Number = "777" },
                        new Phone { Id = 6, Number = "888" }
                    }
                }
            };
        }
    }
}