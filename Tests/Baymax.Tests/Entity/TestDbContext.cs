using Baymax.Entity;
using Microsoft.EntityFrameworkCore;

namespace Baymax.Tests.Entity
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
                : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        
        public DbQuery<PersonView> PersonView { get; set; }
    }
    
    public class PersonView : QueryEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }

    public class Person : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}