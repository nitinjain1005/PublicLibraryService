using PublicLibraryService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicLibraryService.Infrastructure.Data;

namespace PublicLibraryService.TestData.TestDataFactory
{
    public static class TestBookLendingFactory
    {
        public static BookLending GatsbyToAlice => new()
        {
            BookId = 85,
            BorrowerId = 45,
            BorrowedOn = DateTime.UtcNow.AddDays(-25)
        };

        public static BookLending MockingbirdToNitinReturned => new()
        {
            BookId = 86,
            BorrowerId = 42,
            BorrowedOn = DateTime.UtcNow.AddDays(-25),
            ReturnedOn = DateTime.UtcNow.AddDays(-1)
        };

        public static BookLending MockingbirdToAlice1 => new()
        {
            BookId = 86,
            BorrowerId = 45,
            BorrowedOn = DateTime.UtcNow.AddDays(-25)
        };

        public static BookLending MockingbirdToAlice2 => new()
        {
            BookId = 86,
            BorrowerId = 45,
            BorrowedOn = DateTime.UtcNow.AddDays(-25)
        };

        public static List<BookLending> GetAll() =>
            new() { GatsbyToAlice, MockingbirdToNitinReturned, MockingbirdToAlice1, MockingbirdToAlice2 };

        public static BookLending Create(
            int id = 1,
            int bookId = 1,
            int borrowerId = 1,
            DateTime? borrowedOn = null,
            DateTime? returnedOn = null)
        {
            return new BookLending
            {
                Id = id,
                BookId = bookId,
                BorrowerId = borrowerId,
                BorrowedOn = borrowedOn ?? DateTime.UtcNow,
                ReturnedOn = returnedOn ?? DateTime.UtcNow.AddDays(14)
            };
        }

        public static async Task<BookLending> CreateAndSeedAsync(
            PublicLibraryDbContext dbContext,
            Book? book = null,
            Borrower? borrower = null,
            DateTime? borrowedOn = null,
            DateTime? returnedOn = null)
        {
            book ??= await TestBookFactory.CreateAndSeedAsync(dbContext);
            borrower ??= await TestBorrowerFactory.CreateAndSeedAsync(dbContext);

            var lending = Create(
                bookId: book.Id,
                borrowerId: borrower.Id,
                borrowedOn: borrowedOn,
                returnedOn: returnedOn
            );

            dbContext.BookLendings.Add(lending);
            await dbContext.SaveChangesAsync();
            return lending;
        }
    }
}
