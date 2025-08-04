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
            // Add DbContext (SQLite)
            services.AddDbContext<PublicLibraryDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("PublicLibraryDbConnection");
                options.UseSqlite(connectionString);
            });

            // Add repositories, unit of work, etc.
            
            services.AddDbContext<PublicLibraryDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookInventoryRepository, BookInventoryRepository>();
            services.AddScoped<IBookLendingRepository, BookLendingRepository>();


            return services;
        }
    }
}
