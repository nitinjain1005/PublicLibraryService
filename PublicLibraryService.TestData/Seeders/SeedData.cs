using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Infrastructure.Data;
using PublicLibraryService.TestData.TestDataFactory;

namespace PublicLibraryService.TestData.Seeders
{
    public static class SeedData
    {
        public static async Task SeedAllAsync(PublicLibraryDbContext dbContext)
        {
            await TestBookFactory.CreateAndSeedAsync(dbContext, 1, "Book A", 354, "Auther1", default);
            await TestBookFactory.CreateAndSeedAsync(dbContext, 2, "Book B", 54, "Auther2", default);

            await TestBorrowerFactory.CreateAndSeedAsync(dbContext, 1, "Borrower A");
            await TestBorrowerFactory.CreateAndSeedAsync(dbContext, 2, "Borrower B");

            var book1 = await TestBookFactory.CreateAndSeedAsync(dbContext, 3, "Book A3");
            var borrower1 = await TestBorrowerFactory.CreateAndSeedAsync(dbContext, 4, "Borrower A4");


            await TestBookInventoryFactory.CreateAndSeedAsync(dbContext, book1);
            await TestBookLendingFactory.CreateAndSeedAsync(dbContext, book1, borrower1);

            // Add more if needed
        }
    }
}
