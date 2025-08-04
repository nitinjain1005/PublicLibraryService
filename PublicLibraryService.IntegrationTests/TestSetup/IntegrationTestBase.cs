using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PublicLibraryService.Domain.Interfaces;
using PublicLibraryService.Infrastructure.Data;
using PublicLibraryService.Infrastructure.Repositories;

namespace PublicLibraryService.IntegrationTests.TestSetup
{
    public class IntegrationTestBase :IDisposable
    {
        protected readonly ServiceProvider ServiceProvider;
        protected readonly PublicLibraryDbContext publicLibraryDbContextTestDbContext;
        protected readonly IUnitOfWork UnitOfWork;
        private readonly SqliteConnection _connection;
        protected IntegrationTestBase()
        {
           // _connection = new SqliteConnection("DataSource=:memory:;Mode=Memory;Cache=Shared");
            _connection = new SqliteConnection($"DataSource=file:memorydb{Guid.NewGuid()}?mode=memory&cache=shared");
            
            _connection.Open();

            var services = new ServiceCollection();

            services.AddDbContext<PublicLibraryDbContext>(options =>
                options.UseSqlite(_connection));

            services.AddScoped<IBookInventoryRepository, BookInventoryRepository>();
            services.AddScoped<IBookLendingRepository, BookLendingRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            ServiceProvider = services.BuildServiceProvider();

            publicLibraryDbContextTestDbContext = ServiceProvider.GetRequiredService<PublicLibraryDbContext>();
            publicLibraryDbContextTestDbContext.Database.EnsureCreatedAsync();

            UnitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
        }

        public void Dispose()
        {
            publicLibraryDbContextTestDbContext.Database.EnsureDeleted();
            publicLibraryDbContextTestDbContext.Dispose();
            _connection.Dispose();
            ServiceProvider.Dispose();
        }
    }
}
