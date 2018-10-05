using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Baymax.Tester.Integration
{
    public class ApplicationFactory<TStartup, TDbContext> : WebApplicationFactory<TStartup> 
            where TStartup : class
            where TDbContext : DbContext
    {
        public event EventHandler<InitDataEventArgs<TDbContext>> InitDataEvent;
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                                      .AddEntityFrameworkInMemoryDatabase()
                                      .BuildServiceProvider();

                services.AddDbContext<TDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<TDbContext>();

                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();

                    InitDataEvent?.Invoke(this, new InitDataEventArgs<TDbContext>(db));
                }
            });
        }
    }
}