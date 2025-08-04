using Moq;
using PublicLibraryService.Application.Handlers;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Queries;
using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Domain.Interfaces;
using System.Linq.Expressions;

namespace PublicLibraryService.UnitTests.Application.Handlers
{
    public class GetBookAvailabilityHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IGenericRepository<Book>> _mockBookRepo;

        private readonly Mock<IBookInventoryRepository> _mockInventoryRepo;
        private readonly Mock<IBookLendingRepository> _mockLendingRepo;
        private readonly GetBookAvailabilityHandler _handler;
        public GetBookAvailabilityHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockInventoryRepo = new Mock<IBookInventoryRepository>();
            _mockLendingRepo = new Mock<IBookLendingRepository>();

            _mockUnitOfWork.Setup(u => u.BookInventories).Returns(_mockInventoryRepo.Object);
            _mockUnitOfWork.Setup(u => u.BookLendings).Returns(_mockLendingRepo.Object);

            _mockBookRepo = new Mock<IGenericRepository<Book>>();
            _mockUnitOfWork.Setup(u => u.Books).Returns(_mockBookRepo.Object);

            _handler = new GetBookAvailabilityHandler(_mockUnitOfWork.Object);
        }
        [Fact]
        public async Task Handle_ReturnsCorrectAvailability_WhenBookExists()
        {
            // Arrange
            var bookId = 1; // or any valid int value

            _mockBookRepo
                .Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync(new Book
                {
                    Id = bookId,
                    Title = "Testing Book 1",
                    Author = "Au1",
                    PublishedDate = new DateTime(2015, 12, 31),
                    TotalPages = 250
                });

            var inventory = new BookInventory
            {
                BookId = bookId,
                TotalCopies = 10,
                Book = new Book
                {
                    Id = bookId,
                    Title = "Testing Book 1",
                    Author = "Au1",
                    PublishedDate = new DateTime(2015, 12, 31),
                    TotalPages = 250
                }
            };


            _mockInventoryRepo
                .Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<BookInventory, bool>>>()))
                .ReturnsAsync(new List<BookInventory> { inventory });

            _mockLendingRepo
                .Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<BookLending, bool>>>()))
                .ReturnsAsync(new List<BookLending>
                {
                    new BookLending { BookId = bookId, ReturnedOn = null },
                    new BookLending { BookId = bookId, ReturnedOn = null }
                });

            var query = new GetBookAvailabilityQuery(new BookAvailabilityRequest() { BookId = bookId });


            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookId, result.BookId);
            Assert.Equal(10, result.TotalCopies);
            Assert.Equal(2, result.BorrowedCopies);
            Assert.Equal(8, result.AvailableCopies);
        }

        [Fact]
        public async Task Handle_ReturnsNull_WhenBookNotFound()
        {

            // Arrange
            int bookId = 999;

            _mockBookRepo
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new Book
                {
                    Id = 1,
                    Title = "Testing Book 1",
                    Author = "Au1",
                    PublishedDate = new DateTime(2015, 12, 31),
                    TotalPages = 250
                });

            
            var query = new GetBookAvailabilityQuery(new BookAvailabilityRequest() { BookId = bookId });

          
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}

