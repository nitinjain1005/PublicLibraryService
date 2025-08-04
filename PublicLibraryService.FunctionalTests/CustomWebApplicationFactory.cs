using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PublicLibraryService.Infrastructure.Data;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PublicLibraryService.FunctionalTests.TestSetup
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove real DB context
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PublicLibraryDbContext>));
                if (descriptor != null) services.Remove(descriptor);

                // Use SQLite in-memory
               var connection = new SqliteConnection($"DataSource=file:memdb{Guid.NewGuid()}?mode=memory&cache=shared");
                connection.Open();

                services.AddDbContext<PublicLibraryDbContext>(options =>
                {
                    options.UseSqlite(connection);
                });

                // Ensure DB is created
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<PublicLibraryDbContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}

