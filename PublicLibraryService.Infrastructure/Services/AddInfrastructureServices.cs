using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublicLibraryService.Domain.Interfaces;
using PublicLibraryService.Infrastructure.Data;
using PublicLibraryService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Infrastructure.Services
{
    public static class InfrastructureServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var baseDir = AppContext.BaseDirectory;

            var rootPath = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", ".."));

            var publiclibraryPath = Path.Combine(rootPath, "PublicLibraryService.Infrastructure", "Database", "publiclibrary.db");

            // Ensure the folder exists
            var folderPath = Path.GetDirectoryName(publiclibraryPath)!;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Use this path in connection string
            var connectionString = $"Data Source={publiclibraryPath}";

            // Add DbContext (SQLite)
            services.AddDbContext<PublicLibraryDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookInventoryRepository, BookInventoryRepository>();
            services.AddScoped<IBookLendingRepository, BookLendingRepository>();


            return services;
        }
    }
}