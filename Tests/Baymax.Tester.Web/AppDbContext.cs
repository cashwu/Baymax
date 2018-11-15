using Microsoft.EntityFrameworkCore;

namespace Baymax.Tester.Web
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
                : base(options)
        {
            
        }

        public DbSet<Info> Info { get; set; }
    }

    public class Info
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}