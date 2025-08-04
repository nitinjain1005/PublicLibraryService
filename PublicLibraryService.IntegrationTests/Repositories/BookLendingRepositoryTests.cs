using Microsoft.Extensions.DependencyInjection;
using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Domain.Interfaces;
using PublicLibraryService.IntegrationTests.TestSetup;

namespace PublicLibraryService.IntegrationTests.Repositories
{
    public class BookLendingRepositoryTests : IntegrationTestBase
    {
        private readonly IBookLendingRepository _bookLendingRepository;
        
        public BookLendingRepositoryTests()
        {
            _bookLendingRepository = ServiceProvider.GetRequiredService<IBookLendingRepository>();
            SeedTestDataAsync().GetAwaiter().GetResult();

        }
        private async Task SeedTestDataAsync()
        {
            var bookId = 15;

            // Required book and borrowers
            await publicLibraryDbContextTestDbContext.Books.AddAsync(new Book { Id = bookId, Title = "Test Book", Author = "Author", PublishedDate = DateTime.UtcNow, TotalPages = 100 });
            await publicLibraryDbContextTestDbContext.Borrowers.AddRangeAsync(new[]
            {
                new Borrower { Id = 101, Name = "Nitin" },
                new Borrower { Id = 102, Name = "Priya" }
            });

            // Add lendings
            await publicLibraryDbContextTestDbContext.BookLendings.AddRangeAsync(new[]
            {
                new BookLending { Id = 21, BookId = bookId, BorrowerId = 101, BorrowedOn = new DateTime(2025, 06, 07), ReturnedOn = null },
                new BookLending { Id = 22, BookId = bookId, BorrowerId = 102, BorrowedOn = DateTime.UtcNow, ReturnedOn = null },
                new BookLending { Id = 23, BookId = bookId, BorrowerId = 102, BorrowedOn = DateTime.UtcNow, ReturnedOn = DateTime.UtcNow } // returned
            });

            await publicLibraryDbContextTestDbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task CountCurrentlyBorrowedAsync_Returns_CorrectCount()
        {
            // Act
            var count = await _bookLendingRepository.GetBorrowedBooksByBorrowerIdAsync(101,Convert.ToString(new DateTime(2025, 05, 07)),null);

            // Assert
            Assert.Equal(1, count.Count); // two are not returned
        }
    }
}
