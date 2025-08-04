using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PublicLibraryService.Domain.Interfaces;
using PublicLibraryService.FunctionalTests.Helpers;
using PublicLibraryService.Infrastructure.Data;
using System;
using System.Linq;

namespace PublicLibraryService.FunctionalTests.TestSetup
{
    public class CustomWebApplicationFactory: WebApplicationFactory<Program>
    {
        private SqliteConnection _connection;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing DbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PublicLibraryDbContext>));
                if (descriptor != null) services.Remove(descriptor);

                // Open SQLite in-memory connection
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                // Register DbContext using SQLite in-memory
                services.AddDbContext<PublicLibraryDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                // Build and seed
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<PublicLibraryDbContext>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

                db.Database.EnsureCreated();

                try
                {
                    SeedData.InitializeAsync(unitOfWork).Wait();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Seeding error: {Message}", ex.Message);
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _connection?.Close();
                _connection?.Dispose();
            }
        }
        
    }
}

