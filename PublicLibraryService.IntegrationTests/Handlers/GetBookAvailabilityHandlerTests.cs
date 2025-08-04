using PublicLibraryService.Application.Handlers;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.Application.Queries;
using PublicLibraryService.Domain.Entities;
using PublicLibraryService.IntegrationTests.TestSetup;

namespace PublicLibraryService.IntegrationTests.Handlers
{
    public class GetBookAvailabilityHandlerTests : IntegrationTestBase
    {
        public GetBookAvailabilityHandlerTests()
        {
            // Ensure SeedTestData runs before tests
            Task.Run(SeedTestData).GetAwaiter().GetResult();
        }

        private async Task SeedTestData()
        {
            var bookId = 5;

            await publicLibraryDbContextTestDbContext.Books.AddAsync(new Book
            {
                Id = bookId,
                Title = "Testing Integration",
                Author = "Jams",
                PublishedDate = new DateTime(2024, 7, 25),
                TotalPages = 458
            });

            await publicLibraryDbContextTestDbContext.Borrowers.AddRangeAsync(new List<Borrower>
            {
                new Borrower { Id = 100, Name = "Nitin" },
                new Borrower { Id = 101, Name = "Priya" }
            });

            await publicLibraryDbContextTestDbContext.BookInventories.AddAsync(new BookInventory
            {
                Id = 10,
                BookId = bookId,
                TotalCopies = 5
            });

            await publicLibraryDbContextTestDbContext.BookLendings.AddRangeAsync(new List<BookLending>
            {
                new BookLending
                {
                    Id = 12,
                    BookId = bookId,
                    BorrowerId = 100,
                    BorrowedOn = DateTime.UtcNow,
                    ReturnedOn = null
                },
                new BookLending
                {
                    Id = 13,
                    BookId = bookId,
                    BorrowerId = 101,
                    BorrowedOn = DateTime.UtcNow,
                    ReturnedOn = null
                }
            });

            await publicLibraryDbContextTestDbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task GetBookAvailability_Returns_CorrectValues()
        {
            // Arrange
            var handler = new GetBookAvailabilityHandler(UnitOfWork);
            var query = new GetBookAvailabilityQuery(new BookAvailabilityRequest(){BookId = 5});

            // Act
            AvailableBook result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.BookId);
            Assert.Equal(5, result.TotalCopies);
            Assert.Equal(2, result.BorrowedCopies);
            Assert.Equal(3, result.AvailableCopies);
        }
        
    }
}
