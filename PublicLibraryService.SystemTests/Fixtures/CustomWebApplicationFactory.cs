using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Infrastructure.Data;

namespace PublicLibraryService.SystemTests.Fixtures
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private SqliteConnection _connection;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove old DB context registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PublicLibraryDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Create and open SQLite in-memory connection
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                // Register the in-memory SQLite DB
                services.AddDbContext<PublicLibraryDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                // Build service provider and initialize DB
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<PublicLibraryDbContext>();
                db.Database.EnsureCreated(); // important for in-memory SQLite
                SeedTestData(db);
            });
        }
        private void SeedTestData(PublicLibraryDbContext db)
        {
            db.Books.Add(new Book
            {
                Id = 31,
                Title = "System Test Book",
                Author = "Author A",
                PublishedDate = DateTime.Now
            });

            db.BookInventories.Add(new BookInventory
            {
                BookId = 31,
                TotalCopies = 5
            });

            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection?.Dispose();
        }
    }
    
}
