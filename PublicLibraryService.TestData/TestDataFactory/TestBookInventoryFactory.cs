using PublicLibraryService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicLibraryService.Infrastructure.Data;

namespace PublicLibraryService.TestData.TestDataFactory
{
    public static class TestBookInventoryFactory
    {
        public static BookInventory ForGreatGatsby => new()
        {
            BookId = 85,
            TotalCopies = 10
        };

        public static BookInventory ForMockingbird => new()
        {
            BookId = 86,
            TotalCopies = 100
        };

        public static List<BookInventory> GetAll() => new() { ForGreatGatsby, ForMockingbird };

        public static BookInventory Create(
            int id = 1,
            int bookId = 1,
            int totalcopies = 5)
        {
            return new BookInventory
            {
                Id = id,
                BookId = bookId,
                TotalCopies = totalcopies
            };
        }

        public static async Task<BookInventory> CreateAndSeedAsync(
            PublicLibraryDbContext dbContext,
            Book? book = null,
            int quantity = 10)
        {
            book ??= await TestBookFactory.CreateAndSeedAsync(dbContext);

            var inventory = Create(
                bookId: book.Id,
                totalcopies: quantity 
            );

            dbContext.BookInventories.Add(inventory);
            await dbContext.SaveChangesAsync();
            return inventory;
        }
    }
}
